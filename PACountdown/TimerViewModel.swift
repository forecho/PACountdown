import Foundation
import Combine
#if canImport(AppKit)
import AppKit
#endif

class TimerViewModel: ObservableObject {
    // Countdown Timer
    @Published var timeRemaining: TimeInterval = 300 // 5 minutes
    @Published var isTimerRunning = false
    @Published var marketStatusMessage: String = "Market is closed"
    
    // Notification Settings
    @Published var preNotificationSeconds: TimeInterval = 10 {
        didSet {
            UserDefaults.standard.set(Int(preNotificationSeconds), forKey: "preNotificationSeconds")
        }
    }
    @Published var areNotificationsEnabled = true

    // World Clock
    @Published var currentTime: String = "--:--:--"
    
    private var timer: Timer?
    private var marketHoursTimer: Timer?
    private var clockTimer: Timer?
    private let formatter = DateFormatter()
    #if os(macOS)
    private let tickSound = NSSound(named: "Tink")
    private let finalTickSound = NSSound(named: "Glass")
    #endif
    private var lastSecond: Int = -1

    init() {
        // Load saved settings
        if let savedSeconds = UserDefaults.standard.value(forKey: "preNotificationSeconds") as? Int {
            self.preNotificationSeconds = TimeInterval(savedSeconds)
        }

        formatter.dateFormat = "HH:mm:ss"

        // Start checking market hours
        checkMarketHours()
        marketHoursTimer = Timer.scheduledTimer(withTimeInterval: 60, repeats: true) { [weak self] _ in
            self?.checkMarketHours()
        }
        
        // Start the clock
        updateTime()
        clockTimer = Timer.scheduledTimer(withTimeInterval: 1, repeats: true) { [weak self] _ in
            self?.updateTime()
        }
    }
    
    func toggleNotifications() {
        areNotificationsEnabled.toggle()
    }
    
    func testSound() {
        playTickSound()
    }
    
    private func startTimer() {
        guard !isTimerRunning else { return }
        isTimerRunning = true
        calculateAndSetInitialTime()
        lastSecond = Calendar.current.component(.second, from: Date())

        timer?.invalidate()
        // Use a high-frequency timer to check for second changes
        timer = Timer.scheduledTimer(withTimeInterval: 0.05, repeats: true) { [weak self] _ in
            guard let self = self else { return }
            
            let currentSecond = Calendar.current.component(.second, from: Date())
            
            // Only proceed if the system clock's second has changed
            if currentSecond != self.lastSecond {
                self.lastSecond = currentSecond
                
                // Decrement the timer
                self.timeRemaining -= 1
                
                if self.timeRemaining < 0 {
                    self.calculateAndSetInitialTime()
                    return
                }

                // Handle sound notifications
                if self.areNotificationsEnabled {
                    if self.timeRemaining == 0 {
                        // Play final sound when timer hits zero
                        self.playFinalTickSound()
                    } else if self.timeRemaining < self.preNotificationSeconds {
                        // Play tick sound during the notification period (but not at zero)
                        self.playTickSound()
                    }
                }
                
                // Reset the timer for the next 5-minute interval *after* it hits zero
                if self.timeRemaining == 0 {
                    DispatchQueue.main.asyncAfter(deadline: .now() + 0.5) {
                        self.calculateAndSetInitialTime()
                    }
                }
            }
        }
        RunLoop.current.add(timer!, forMode: .common)
    }
    
    private func stopTimer() {
        isTimerRunning = false
        timer?.invalidate()
        timer = nil
        lastSecond = -1
    }
    
    private func calculateAndSetInitialTime() {
        let now = Date()
        let calendar = Calendar.current
        
        let minute = calendar.component(.minute, from: now)
        let second = calendar.component(.second, from: now)
        
        let secondsIntoInterval = (minute % 5) * 60 + second
        
        // Ensure timeRemaining is never negative
        timeRemaining = max(0, 300 - TimeInterval(secondsIntoInterval))
    }
    
    private func checkMarketHours() {
        if isMarketOpen() {
            marketStatusMessage = "Market is open"
            startTimer()
        } else {
            marketStatusMessage = "Market is closed"
            stopTimer()
        }
    }
    
    private func isMarketOpen() -> Bool {
        let now = Date()
        var calendar = Calendar.current
        guard let easternTimeZone = TimeZone(identifier: "America/New_York") else { return false }
        calendar.timeZone = easternTimeZone
        
        let components = calendar.dateComponents([.weekday, .hour, .minute], from: now)
        guard let weekday = components.weekday, (2...6).contains(weekday) else { return false }
        
        guard let hour = components.hour, let minute = components.minute else { return false }
        
        let currentTimeInMinutes = hour * 60 + minute
        let marketOpenTimeInMinutes = 9 * 60 + 30
        let marketCloseTimeInMinutes = 16 * 60
        
        return currentTimeInMinutes >= marketOpenTimeInMinutes && currentTimeInMinutes < marketCloseTimeInMinutes
    }
    
    private func playTickSound() {
        #if os(macOS)
        tickSound?.stop()
        tickSound?.play()
        #endif
    }

    private func playFinalTickSound() {
        #if os(macOS)
        finalTickSound?.stop()
        finalTickSound?.play()
        #endif
    }

    private func updateTime() {
        currentTime = formatter.string(from: Date())
    }
    
    deinit {
        timer?.invalidate()
        marketHoursTimer?.invalidate()
        clockTimer?.invalidate()
    }
} 

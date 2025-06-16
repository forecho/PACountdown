import SwiftUI

struct ContentView: View {
    @StateObject private var viewModel = TimerViewModel()

    var body: some View {
        VStack(spacing: 20) {
            // World Clock
            VStack(spacing: 8) {
                Text(viewModel.currentTime)
                    .font(.system(size: 36, weight: .light, design: .monospaced))
                    .foregroundColor(.primary)
            }
            .padding(.vertical, 20)
            .frame(maxWidth: .infinity)
            .background(Color(.windowBackgroundColor))
            .cornerRadius(12)
            
            // Countdown Timer
            VStack(spacing: 12) {
                Text("5-Min Interval Countdown")
                    .font(.headline)
                    .foregroundColor(.secondary)
                
                Text(timeString(time: viewModel.timeRemaining))
                    .font(.system(size: 80, weight: .bold, design: .monospaced))
                    .foregroundColor(.primary)
                
                Text(LocalizedStringKey(viewModel.marketStatusMessage))
                    .font(.subheadline)
                    .foregroundColor(viewModel.isTimerRunning ? .green : .red)
            }
            .padding(.vertical, 20)
            .frame(maxWidth: .infinity)
            .background(Color(.windowBackgroundColor))
            .cornerRadius(12)

            // Controls
            VStack(spacing: 16) {
                HStack(spacing: 12) {
                    Button(action: {
                        viewModel.toggleNotifications()
                    }) {
                        Label(viewModel.areNotificationsEnabled ? "Mute Notifications" : "Unmute Notifications",
                              systemImage: viewModel.areNotificationsEnabled ? "speaker.slash.fill" : "speaker.wave.2.fill")
                            .frame(maxWidth: .infinity)
                    }
                    .buttonStyle(.borderedProminent)
                    .tint(viewModel.areNotificationsEnabled ? .orange : .indigo)
                    
                    Button(action: {
                        viewModel.testSound()
                    }) {
                        Label("Test Sound", systemImage: "speaker.wave.2.fill")
                    }
                    .buttonStyle(.bordered)
                }
                
                VStack(alignment: .leading, spacing: 8) {
                    Text("Notification Settings")
                        .font(.headline)
                        .foregroundColor(.secondary)
                    
                    Stepper(value: $viewModel.preNotificationSeconds, in: 1...60, step: 1) {
                        Text(String(format: NSLocalizedString("Pre-notification time: %lld seconds", comment: ""), Int(viewModel.preNotificationSeconds)))
                    }
                }
                .padding(.horizontal, 8)
            }
            .padding(.vertical, 20)
            .frame(maxWidth: .infinity)
            .background(Color(.windowBackgroundColor))
            .cornerRadius(12)
        }
        .padding(20)
        .frame(minWidth: 480, minHeight: 500)
    }

    private func timeString(time: TimeInterval) -> String {
        let minutes = Int(time) / 60 % 60
        let seconds = Int(time) % 60
        return String(format: "%02i:%02i", minutes, seconds)
    }
}

extension TimeZone: @retroactive Identifiable {
    public var id: String { identifier }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
} 

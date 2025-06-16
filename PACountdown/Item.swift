//
//  Item.swift
//  PACountdown
//
//  Created by forecho on 2025/6/17.
//

import Foundation
import SwiftData

@Model
final class Item {
    var timestamp: Date
    
    init(timestamp: Date) {
        self.timestamp = timestamp
    }
}

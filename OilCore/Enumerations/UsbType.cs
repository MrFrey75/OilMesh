namespace OilCore.Enumerations;

public enum UsbType
{
    Unknown,        // Default
    USB_A,         // Standard rectangular USB Type-A
    USB_B,         // Square-shaped USB Type-B (printers, scanners)
    USB_MiniA,     // Mini-USB Type-A (legacy devices)
    USB_MiniB,     // Mini-USB Type-B (older phones, cameras)
    USB_MicroA,    // Micro-USB Type-A (rare, mostly obsolete)
    USB_MicroB,    // Micro-USB Type-B (older Android phones, accessories)
    USB_C,         // Reversible USB Type-C (modern standard)
    USB_3_A,       // USB 3.0 Type-A (blue port, faster speeds)
    USB_3_B,       // USB 3.0 Type-B (larger, for some external drives)
    USB_3_MicroB,  // USB 3.0 Micro-B (external hard drives)
    USB_4,         // Latest USB 4 standard (integrated with Thunderbolt 3)
    Thunderbolt3,  // Thunderbolt 3 (USB-C connector)
    Thunderbolt4   // Thunderbolt 4 (USB-C connector, more features)
}
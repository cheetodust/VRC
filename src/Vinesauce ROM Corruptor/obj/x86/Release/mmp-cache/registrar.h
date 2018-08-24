#pragma clang diagnostic ignored "-Wdeprecated-declarations"
#pragma clang diagnostic ignored "-Wtypedef-redefinition"
#pragma clang diagnostic ignored "-Wobjc-designated-initializers"
#include <stdarg.h>
#include <objc/objc.h>
#include <objc/runtime.h>
#include <objc/message.h>
#import <Foundation/Foundation.h>
#import <CloudKit/CloudKit.h>
#import <AppKit/AppKit.h>
#import <CoreGraphics/CoreGraphics.h>

@class NSApplicationDelegate;
@protocol NSMenuValidation;
@class NSTableViewDataSource;
@class NSTableViewDelegate;
@class __monomac_internal_ActionDispatcher;
@class NSURLSessionDataDelegate;
@class __MonoMac_NSActionDispatcher;
@class __MonoMac_NSAsyncActionDispatcher;
@class __NSGestureRecognizerToken;
@class __NSClickGestureRecognizer;
@class __NSGestureRecognizerParameterlessToken;
@class __NSGestureRecognizerParametrizedToken;
@class __NSMagnificationGestureRecognizer;
@class __NSPanGestureRecognizer;
@class __NSPressGestureRecognizer;
@class __NSRotationGestureRecognizer;
@class AppKit_NSTableView__NSTableViewDelegate;
@class Foundation_NSUrlSessionHandler_WrappedNSInputStream;
@class __NSObject_Disposer;
@class Foundation_NSUrlSessionHandler_NSUrlSessionHandlerDelegate;
@class AppDelegate;
@class Corruptor_RomDirDataSource;
@class Corruptor_RomDirDelegate;
@class ViewController;

@interface NSApplicationDelegate : NSObject<NSApplicationDelegate> {
}
	-(id) init;
@end

@protocol NSMenuValidation
	@required -(BOOL) validateMenuItem:(NSMenuItem *)p0;
@end

@interface NSTableViewDataSource : NSObject<NSTableViewDataSource> {
}
	-(id) init;
@end

@interface NSTableViewDelegate : NSObject<NSTableViewDelegate> {
}
	-(id) init;
@end

@interface NSURLSessionDataDelegate : NSObject<NSURLSessionDataDelegate, NSURLSessionTaskDelegate, NSURLSessionDelegate> {
}
	-(id) init;
@end

@interface __NSGestureRecognizerToken : NSObject {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface __NSGestureRecognizerParameterlessToken : __NSGestureRecognizerToken {
}
	-(void) target;
@end

@interface __NSGestureRecognizerParametrizedToken : __NSGestureRecognizerToken {
}
	-(void) target:(NSGestureRecognizer *)p0;
@end

@interface AppDelegate : NSObject<NSApplicationDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(void) applicationDidFinishLaunching:(NSNotification *)p0;
	-(void) applicationWillTerminate:(NSNotification *)p0;
	-(BOOL) applicationShouldTerminateAfterLastWindowClosed:(NSApplication *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface Corruptor_RomDirDataSource : NSObject<NSTableViewDataSource> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) numberOfRowsInTableView:(NSTableView *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface Corruptor_RomDirDelegate : NSObject<NSTableViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSView *) tableView:(NSTableView *)p0 viewForTableColumn:(NSTableColumn *)p1 row:(NSInteger)p2;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface ViewController : NSViewController {
}
	@property (nonatomic, assign) NSButton * AddRadioButton;
	@property (nonatomic, assign) NSTextField * AddXtoByteField;
	@property (nonatomic, assign) NSButton * AutoEndCheckbox;
	@property (nonatomic, assign) NSView * ByteCorruptionView;
	@property (nonatomic, assign) NSTextField * EmulatorField;
	@property (nonatomic, assign) NSTextField * EmulatorLabel;
	@property (nonatomic, assign) NSButton * EmulatorSelectButtonOutlet;
	@property (nonatomic, assign) NSButton * EnableCpuJamCheckbox;
	@property (nonatomic, assign) NSButton * EndByteDownButton;
	@property (nonatomic, assign) NSTextField * EndByteField;
	@property (nonatomic, assign) NSTextField * EndByteLabel;
	@property (nonatomic, assign) NSButton * EndByteUpButton;
	@property (nonatomic, assign) NSTextField * EveryNthByteField;
	@property (nonatomic, assign) NSTextField * IncrementField;
	@property (nonatomic, assign) NSButton * RangeDownButton;
	@property (nonatomic, assign) NSButton * RangeUpButton;
	@property (nonatomic, assign) NSTextField * ReplaceByteXwithYByteXField;
	@property (nonatomic, assign) NSTextField * ReplaceByteXwithYByteYField;
	@property (nonatomic, assign) NSButton * ReplaceRadioButton;
	@property (nonatomic, assign) NSTextField * RomDirField;
	@property (nonatomic, assign) NSTableView * RomDirTable;
	@property (nonatomic, assign) NSTableColumn * RomDirTableColumn;
	@property (nonatomic, assign) NSTextField * RomSaveField;
	@property (nonatomic, assign) NSButton * ShiftRightRadioButton;
	@property (nonatomic, assign) NSTextField * ShiftRightXBytesField;
	@property (nonatomic, assign) NSButton * StartByteDownButton;
	@property (nonatomic, assign) NSTextField * StartByteField;
	@property (nonatomic, assign) NSButton * StartByteUpButton;
	@property (nonatomic, assign) NSButton * ToggleByteCorruptionButton;
	@property (nonatomic, assign) NSButton * ToggleEmulatorButton;
	@property (nonatomic, assign) NSButton * ToggleOverwriteFile;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSButton *) AddRadioButton;
	-(void) setAddRadioButton:(NSButton *)p0;
	-(NSTextField *) AddXtoByteField;
	-(void) setAddXtoByteField:(NSTextField *)p0;
	-(NSButton *) AutoEndCheckbox;
	-(void) setAutoEndCheckbox:(NSButton *)p0;
	-(NSView *) ByteCorruptionView;
	-(void) setByteCorruptionView:(NSView *)p0;
	-(NSTextField *) EmulatorField;
	-(void) setEmulatorField:(NSTextField *)p0;
	-(NSTextField *) EmulatorLabel;
	-(void) setEmulatorLabel:(NSTextField *)p0;
	-(NSButton *) EmulatorSelectButtonOutlet;
	-(void) setEmulatorSelectButtonOutlet:(NSButton *)p0;
	-(NSButton *) EnableCpuJamCheckbox;
	-(void) setEnableCpuJamCheckbox:(NSButton *)p0;
	-(NSButton *) EndByteDownButton;
	-(void) setEndByteDownButton:(NSButton *)p0;
	-(NSTextField *) EndByteField;
	-(void) setEndByteField:(NSTextField *)p0;
	-(NSTextField *) EndByteLabel;
	-(void) setEndByteLabel:(NSTextField *)p0;
	-(NSButton *) EndByteUpButton;
	-(void) setEndByteUpButton:(NSButton *)p0;
	-(NSTextField *) EveryNthByteField;
	-(void) setEveryNthByteField:(NSTextField *)p0;
	-(NSTextField *) IncrementField;
	-(void) setIncrementField:(NSTextField *)p0;
	-(NSButton *) RangeDownButton;
	-(void) setRangeDownButton:(NSButton *)p0;
	-(NSButton *) RangeUpButton;
	-(void) setRangeUpButton:(NSButton *)p0;
	-(NSTextField *) ReplaceByteXwithYByteXField;
	-(void) setReplaceByteXwithYByteXField:(NSTextField *)p0;
	-(NSTextField *) ReplaceByteXwithYByteYField;
	-(void) setReplaceByteXwithYByteYField:(NSTextField *)p0;
	-(NSButton *) ReplaceRadioButton;
	-(void) setReplaceRadioButton:(NSButton *)p0;
	-(NSTextField *) RomDirField;
	-(void) setRomDirField:(NSTextField *)p0;
	-(NSTableView *) RomDirTable;
	-(void) setRomDirTable:(NSTableView *)p0;
	-(NSTableColumn *) RomDirTableColumn;
	-(void) setRomDirTableColumn:(NSTableColumn *)p0;
	-(NSTextField *) RomSaveField;
	-(void) setRomSaveField:(NSTextField *)p0;
	-(NSButton *) ShiftRightRadioButton;
	-(void) setShiftRightRadioButton:(NSButton *)p0;
	-(NSTextField *) ShiftRightXBytesField;
	-(void) setShiftRightXBytesField:(NSTextField *)p0;
	-(NSButton *) StartByteDownButton;
	-(void) setStartByteDownButton:(NSButton *)p0;
	-(NSTextField *) StartByteField;
	-(void) setStartByteField:(NSTextField *)p0;
	-(NSButton *) StartByteUpButton;
	-(void) setStartByteUpButton:(NSButton *)p0;
	-(NSButton *) ToggleByteCorruptionButton;
	-(void) setToggleByteCorruptionButton:(NSButton *)p0;
	-(NSButton *) ToggleEmulatorButton;
	-(void) setToggleEmulatorButton:(NSButton *)p0;
	-(NSButton *) ToggleOverwriteFile;
	-(void) setToggleOverwriteFile:(NSButton *)p0;
	-(void) viewDidLoad;
	-(NSObject *) representedObject;
	-(void) setRepresentedObject:(NSObject *)p0;
	-(void) AutoEnd:(NSButton *)p0;
	-(void) ByteRangeDown:(NSObject *)p0;
	-(void) ByteRangeUp:(NSObject *)p0;
	-(void) CorruptionOption:(NSObject *)p0;
	-(void) EmulatorSelectButton:(NSObject *)p0;
	-(void) EndByteDecrease:(NSObject *)p0;
	-(void) EndByteIncrease:(NSObject *)p0;
	-(void) GithubCheetodust:(NSObject *)p0;
	-(void) GithubRikerz:(NSObject *)p0;
	-(void) RomDirButton:(NSObject *)p0;
	-(void) RomDirTableAction:(NSObject *)p0;
	-(void) RomSaveButton:(NSObject *)p0;
	-(void) RunButton:(NSObject *)p0;
	-(void) StartByteDecrease:(NSObject *)p0;
	-(void) StartByteIncrease:(NSObject *)p0;
	-(void) ToggleByteCorruption:(NSButton *)p0;
	-(void) ToggleEmulator:(NSObject *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end



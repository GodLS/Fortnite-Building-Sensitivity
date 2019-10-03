#SingleInstance force
#include MouseDelta.ahk
#IfWinActive, Fortnite

; ============= START USER-CONFIGURABLE SECTION =============
; Keys you want to raise sensitivity when pressed. Build binds, edit, pickaxe, etc.
; Comment out any keys you dont want or need with ; before the line


BuildKey0 := "~Q"
BuildKey1 := "~W"
BuildKey2 := "~E"
BuildKey3 := "~R"

; Keys you want to go back to normal sensitivity when pressed. Weapons, items, edit, pickaxe, etc.
; Comment out any keys you dont want or need with ; before the line

WeaponKey0 := "~1"
WeaponKey1 := "~2"
WeaponKey2 := "~3"
WeaponKey3 := "~4"



ScaleFactor := 1.5312 ;The amount to multiply movement by when in Build Mode

; ============= END USER-CONFIGURABLE SECTION =============

; Adjust ScaleFactor. If the user wants 2x sensitivity, we only need to send 1x input...
; ... because the user already moved the mouse once, so we only need to send that input 1x more...
; ... to achieve 2x sensitivity

ScaleFactor -= 1
WeaponMode := 1
md := new MouseDelta("MouseEvent").Start()

; If you need more keys, add the next line and so on "hotkey, % BuildKey6, BuildStart"
; Comment out any keys you dont want or need with ; before the line


hotkey, % BuildKey0, BuildStart
hotkey, % BuildKey1, BuildStart
hotkey, % BuildKey2, BuildStart
hotkey, % BuildKey3, BuildStart

; If you need more keys, add the next line and so on "hotkey, % WeaponKey8, BuildStop"
; Comment out any keys you dont want or need with ; before the line


hotkey, % WeaponKey0, BuildStop
hotkey, % WeaponKey1, BuildStop
hotkey, % WeaponKey2, BuildStop
hotkey, % WeaponKey3, BuildStop
 
BuildStart:
	WeaponMode := 1
	md.SetState(WeaponMode)
	return

BuildStop:
	md.SetState(!WeaponMode)
	return

; Gets called when mouse moves or stops
; x and y are DELTA moves (Amount moved since last message), NOT coordinates.
MouseEvent(MouseID, x := 0, y := 0){
	global ScaleFactor
 
	if (MouseID){
		x *= ScaleFactor, y *= ScaleFactor
		DllCall("mouse_event",uint,1,int, x ,int, y,uint,0,int,0)
	}
}

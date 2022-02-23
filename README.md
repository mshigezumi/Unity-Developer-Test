# Unity Developer Test

My submition for this test can be found above in the Submission_MichaelShigezumi folder.

Below is the test as given by Feel Free Games.

## Unity developer test

This test is designed in such a way that our perfect candidate will feel right at home.

Implementing user interfaces and getting that pixel art pixel-perfect look are often regarded as tedious tasks.

Is this something that is right up your alley? Then you might be the candidate we are looking for.

We would like you to implement a simple inventory menu in Unity.

It is up to you to decide how to make the interactions clear to the player and how to add extra polish, for example by using sound and/or animations.

We expect you to be self-reliant enough to be able to finish this test without needing to contact us again for more details.

If certain parts of the test aren’t clear, try to make the best of it using your own insights.

Make sure that we can evaluate your C# skills in your submission.

GOAL:

The goals of this test are to test your ability to:

* Follow instructions.
* Complete a simple but tedious UI task.
* Polish the UI interactions to elevate the experience to the next level.

FILES:

* Use this free PNG file for the inventory menu background.
* Use this free PNG file for highlighting a selected inventory slot.
* Use the 16x16 versions from this free sample inventory icon pack for the inventory items (scroll down for the free version).
* Use this font for text.

RULES:

* You will be using the provided PNG files and font.
* All text should appear to match the resolution of the inventory menu.

BRIEF:

Please use the provided/linked files to create a centered pixel art pixel-perfect inventory menu.

When you run your application, 5 random 16x16 icons from the inventory icon pack must be placed in 5 random inventory slots. The inventory slot in the top left corner should be selected by default, even if it is empty.

Inputs:

* If you press the Y button on the Xbox controller, all inventory slots should be cleared and 5 random 16x16 icons from the inventory icon pack must be placed in 5 random inventory slots again.
* You should be able to use the up/down/left/right directions on the joysticks and the D-pad on the Xbox controller to select a different inventory slot.
* If you press the A button on the Xbox controller while an inventory slot with an item is selected, the item should be picked up and can then be placed in any other inventory slot via the up/down/left/right directions on the joysticks and the D-pad. 
* If the inventory slot that the item is being placed in already contains an item, the items should swap positions before being placed in the respective slots.
* If you press the Y button while an item is picked up, the item should disappear.
* If you press the left and right shoulder buttons respectively on the Xbox controller, it should cycle through the required resolutions (1280x720, 1920x1080, 3860x2160) whilst scaling all elements accordingly to ensure that they are legible and pixel-perfect. The inventory menu should cover no less than 25% of the screen at the listed resolutions.

Text:

* The text ‘Inventory’ should be displayed above the top left corner of the inventory menu as left-aligned text.
* If an item is selected in an inventory slot, the name of the selected item should be displayed below the bottom right corner of the inventory menu as right-aligned text (you will need to manually assign an appropriate name to each of the 55 icons).

## Scripts

### PlayerControls.cs

This is the automaticly generated script using the newer Unity input system.

### AudioManager.cs

This is a simple audio manager using a singleton pattern which will stay loaded on the scene even on scene switch. Takes an array of Sound to help keep track of sound files and play them when needed.

### Sound.cs

Wrapper for audio files for use with the audio manager.

### Inventory.cs



### InventorySlot.cs

### InventoryUI.cs

### Item.cs

### ItemPickup.cs

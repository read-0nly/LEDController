# LEDController
Control HappyLighting and HiLighting protocol BLE LED lights

Replaces these apps in particular:

- https://play.google.com/store/apps/details?id=com.xiaoyu.hlight
- https://play.google.com/store/apps/details?id=com.lixing.hilighting

I could try to explain the whole UI or I could just give you a really shitty mspaint breakdown, here you go
<img width="752" height="687" alt="image" src="https://github.com/user-attachments/assets/af24d62f-2ef2-491d-9efe-e792146c6f1b" />

Right now only Time Gradient is implemented so all devices will default to that behavior. If there's no protocol, nothing gets sent on writecolor. So if it's not your lights, leave it on None and leave the Tick selector unchecked to not blast it with weird traffic

The time gradient behavior will launch a file selector on Init. Select a bitmap, it will then find the pixel index of the current minute of the day and the next minute based on the image width. Ideally, the image should be 1440x1, but can be anything. It then lerps the color between the two pixels based on how many seconds deep we are in the current minute, for a relatively smooth transition.

It accepts only an hour offset as parameter, no ; or anything - it's just doing int.parse without checking right now so if you don't listen it'll crash. But it accepts negative offsets too

For instance, here's the time gradient I use (notice how it spits in the face of the dimensions above). I use an offset of -8, so it starts the loop in the dark red at midnight, does that for 8 hours, then wraps araound to the left-hand side when it reaches 8am and I get blasted by white light. Very roommate-friendly way to wake up as long as you don't actually share the room. Basically, using this, the offset is negative the time you plan to wake up.

<img width="96" height="36" alt="t4fdsggsd" src="https://github.com/user-attachments/assets/20fcd09d-4973-4d14-bd8e-a8d6a75030ee" />

Alert will alternate between the last color set and the inverse of that color - so white will just flash on and off, red will bounce to cyan, etc. Pretty jarring, does what it should.

Save file format is very dumb atm. Example line from savedLEDs.txt

Bluetooth FF:FF:FF:FF:FF:FF;99999999999999;HappyLighting;False

The format is "NAME;ADDRESS;PROTOCOL;HIDDEN". One device per line. Setting the last value to "True" will make it so the BLE watcher immediately if ors that device if it's seen - a way to get rid of recurring devices that aren't your lights. Some devices rotate their address so they'll keep popping up - i don't have a good solution for that atm.

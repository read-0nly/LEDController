# LEDController
Control HappyLighting and HiLighting protocol BLE LED lights (If you have lights using different protocols, send me the Google Play link to the app in issues and I'll see what I can do)

Replaces these apps in particular:

- https://play.google.com/store/apps/details?id=com.xiaoyu.hlight
- https://play.google.com/store/apps/details?id=com.lixing.hilighting

I could try to explain the whole UI or I could just give you a really shitty mspaint breakdown, here you go
<img width="752" height="687" alt="image" src="https://github.com/user-attachments/assets/af24d62f-2ef2-491d-9efe-e792146c6f1b" />

First thing to do is click the scan button to find some devices. Once you found your devices, stop scanning.

Right now only Time Gradient is implemented so all devices will default to that behavior. If there's no protocol, nothing gets sent on writecolor. So if it's not your lights, leave it on None and leave the Tick selector unchecked to not blast it with weird traffic. Hit the X button top-right to then hide it so it doesn't clutter your interface.

The time gradient behavior will launch a file selector on Init. Select a bitmap, it will then find the pixel index of the current minute of the day and the next minute based on the image width. Ideally, the image should be 1440x1, but can be anything. It then lerps the color between the two pixels based on how many seconds deep we are in the current minute, for a relatively smooth transition.

It accepts only an hour offset as parameter, no ; or anything - it's just doing int.parse without checking right now so if you don't listen it'll crash. But it accepts negative offsets too. The parameter field needs to be filled before clicking init - if you do it backwards just click init again and cancel the file selector, it'll apply the new value to the offset but keep the same image.

For instance, here's the time gradient I use (notice how it spits in the face of the dimensions above). I use an offset of -8, so it starts the loop in the dark red at midnight, does that for 8 hours, then wraps araound to the left-hand side when it reaches 8am and I get blasted by white light. Very roommate-friendly way to wake up as long as you don't actually share the room. Basically, using this, the offset is negative the time you plan to wake up.

<img width="96" height="36" alt="t4fdsggsd" src="https://github.com/user-attachments/assets/20fcd09d-4973-4d14-bd8e-a8d6a75030ee" />

And here's what the device looks like in the interface

<img width="136" height="152" alt="image" src="https://github.com/user-attachments/assets/79a79c04-1380-406e-8aaf-73b867444fa2" />

Alert will alternate between the last color set and the inverse of that color - so white will just flash on and off, red will bounce to cyan, etc. Pretty jarring, does what it should.

The server, if turned on, will host a page at port :8080 locally from which you can trigger the alert, start ticking, and kill the server (turn it back off - app keeps running unaffected)

People on your LAN can then use this page to trigger the light alerts and get your attention if for instance you've got headphones on. If you visit /StartAlert directly it'll do the same so it's also possible to make your own stuff trigger the alert based on external events (new messages in discord/fb, calendar events, you name it - if you can make code detect the event you want and send a request to that url when that event happens, you're golden)

Save file format is very dumb atm. Example line from savedLEDs.txt

Bluetooth FF:FF:FF:FF:FF:FF;99999999999999;HappyLighting;False

The format is "NAME;ADDRESS;PROTOCOL;HIDDEN". One device per line. Setting the last value to "True" will make it so the BLE watcher immediately if ors that device if it's seen - a way to get rid of recurring devices that aren't your lights. Some devices rotate their address so they'll keep popping up - i don't have a good solution for that atm.

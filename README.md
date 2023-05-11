# FictionMobile
Mobile[ANDROID] companion app for FictionHoarder app. Created using .NET Maui.

## Using .NET MAUI
So... I decided to start learning mobile development and .NET MAUI was the natural choice when your keeping yourself within .NET. 
Before beginning this project I has already created multiple practice projects to get a better grasp of this framework. Once those were done I decided
that I could begin working on a mobile comapanion for my desktop app, FictionHoarder.

## Early Compromises
When I began this project I initially thought I could use FictionHoarder's UI Class Library, but I was soon met with some issues that prevented this. 
For one, the library had certain lines of code within important methods that I didn't need nor want for my mobile app and then there was the problem of 
the library being a WPF class library that wasn't compatible with MAUI apps.

To overcome this I had to create a MAUI class library for my app and transfer most of the code over. I made the necessary modifications and implemented
the new library into my MAUI project.

I also came upon a probelm with connecting to my API that required me to use a dangerous workaround that should only be run in debug mode. There is also
the fact that I have to connect to my IP address to use my phyiscal phone for testing which led me to changing the listening endpoints in my API project.

## Designing a Mobile App
When designing my app I wanted it to have a similar design scheme to FictionHoarder so I used the same color as I did in my desktop app. Many of the
views found in FictionHoarder can also be found in FictionMobile. All views are displayed on a singular page for the user to choose from.

<img src="https://github.com/AndyJL1999/FictionMobile/assets/88408654/f6727456-b429-40a1-9102-7396762795c6" />

There is of-course the LoginView that comes before anything else and it too is similar to it's desktop counterpart with two seperate forms on one page
that handle registering and logging in. 

<img src="https://github.com/AndyJL1999/FictionMobile/assets/88408654/261938fb-4700-450e-813d-e465d22c36e5" />

I wanted this app to be easy to navigate and easy to understand which led to this simplistic flat design.

## Does It Work?
As of right now the app performs all the functions I set out to implement. The user can sign in or register and upload, read, or delete stories. 
The reading itself involves swiping through chapters, scrolling up and down on indivdual chapters, and navigating through chapters with a 
flyout menu. 

There are still some limitations that I have when trying to modify certain Epub files. This app works best with fanfiction, but can support professional
works.

## What's Next?
While working on this project I found myself questioning many of my previous design decisions when creating the original FictionHoarder app
and have decided to rework it. Now that I've completed this mobile app I'll be evaluating my old desktop app and changing the structure to 
better suit the requirements I originally setup for it. This will propably involve many of the old sql queries and the class libraries that
I set up for the app's use.

## Demonstration
I have this video on youtube: https://youtu.be/IN-M2XnLSeo. It demonstrates most of the apps functionality. I forgot to show the HistoryView
but it shouldn't be too big a deal since it is very similar to the StoriesView. 

https://github.com/AndyJL1999/FictionMobile/assets/88408654/dc25b119-0f23-4d85-8776-ccf6e7a0532c



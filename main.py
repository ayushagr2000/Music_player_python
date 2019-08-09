import os
from tkinter import *
import tkinter.messagebox
from tkinter import filedialog
from pygame import mixer
root = Tk()

menubar = Menu(root)
root.config(menu=menubar)
subMenu = Menu(menubar,tearoff=0)


def browse_file():
    global filename 
    filename = filedialog.askopenfilename()
    print(filename)
    
menubar.add_cascade(label="File",menu=subMenu)
subMenu.add_command(label="Open",command = browse_file)
subMenu.add_command(label="Exit", command =root.destroy)
def about_us():
    tkinter.messagebox.showinfo('About Melody', 'This is a music player build using Python')
    
    
subMenu = Menu(menubar,tearoff=0)
menubar.add_cascade(label="help ",menu=subMenu)
subMenu.add_command(label="About Us",command =about_us)



mixer.init() 

root.title("Music Player")
root.iconbitmap(r'images/play.ico')
text = Label(root,text = 'Music Player')
text.pack(pady=10)

def playmusic():
    global paused
    
    if paused:
        mixer.music.unpause()
        print("This button is pause")
        statusbar['text'] = "Music Resumed"
        paused = FALSE
    else:
        try:
            mixer.music.load(filename)
            mixer.music.play()
            print("This button is play")
            statusbar['text'] = "Playing Music: "+os.path.basename(filename)
                
        except:
            tkinter.messagebox.showerror('Error: Open some song first')
            print("Error: Open some song first")

paused = False

def stopmusic():
    mixer.music.stop()
    print("This button is working")
    statusbar['text'] = "Music Stopped"

def pausemusic():
    global paused
    paused = TRUE 
    mixer.music.pause()
    statusbar['text'] = "Music Paused"

    
def setvol(val):
    volume = int(val)/100
    mixer.music.set_volume(volume)
    

middleframe = Frame(root)
middleframe.pack(padx =10,pady = 10)

playphoto = PhotoImage(file='images/playb.png')    
playbtn = Button(middleframe,image=playphoto,command = playmusic)
playbtn.grid(row=0,column=0,padx=10)


stopphoto = PhotoImage(file='images/stop.png')    
stopbtn = Button(middleframe,image=stopphoto,command = stopmusic)
stopbtn.grid(row=0,column=1, padx=10)

pausephoto = PhotoImage(file='images/pause.png')    
pausebtn = Button(middleframe,image=pausephoto,command = pausemusic)
pausebtn.grid(row=0,column=2, padx=10)

scale = Scale(root,from_=0,to =100, orient = HORIZONTAL,command = setvol)
scale.set(70)
mixer.music.set_volume(0.2)
scale.pack(pady =15)

statusbar = Label(root,text = "Wellcome",relief= SUNKEN)
statusbar.pack(side=BOTTOM,fill = X,anchor=W)
root.mainloop()



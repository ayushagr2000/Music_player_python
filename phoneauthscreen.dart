import 'package:firebase_auth/firebase_auth.dart';

import 'package:flutter/material.dart';

import 'package:registration/Screens/Myhomepage.dart';
import 'package:registration/Screens/location.dart';


class Authentication extends StatefulWidget {

  @override

  _AuthenticationState createState() => _AuthenticationState();

}



class _AuthenticationState extends State<Authentication> {

showAlertDialog(BuildContext context,String text) {

  // set up the buttons
  
  Widget continueButton = FlatButton(
    child: Text("Ok"),
    onPressed:  () {
      Navigator.of(context).pop();
    },
  );

  // set up the AlertDialog
  AlertDialog alert = AlertDialog(
    title: Text("AlertDialog"),
    content: Text("$text"),
    actions: [
     
      continueButton,
    ],
  );

  // show the dialog
  showDialog(
    context: context,
    builder: (BuildContext context) {
      return alert;
    },
  );
}

  String phoneNo;

  String smsCode;

  String verificationId;



  Future<void> verifyNumber() async{

  

    final PhoneCodeAutoRetrievalTimeout autoRetrieve=(String verID){

        this.verificationId=verID;

        ///Dialog here

        smsCodeDialog(context);

    };



    final PhoneVerificationCompleted verificationSuccess=(AuthCredential credential){

      print("Verified");

      showAlertDialog(context, "Verified pvc");



      Navigator.pushReplacement(context, MaterialPageRoute(builder: (BuildContext context)=>

      Location()

      ));

    };



    final PhoneCodeSent smsCodeSent=(String verID,[int forceCodeResend]){

      this.verificationId=verID;

     // Navigator.pop(context);
      smsCodeDialog(context);


    //  Navigator.pushReplacement(context, MaterialPageRoute(builder: (BuildContext context)=>

    //  MyHomePage()) );

    };



    final PhoneVerificationFailed verificationFailed=(AuthException exception){

      print(exception.message);
      print(exception.message);
      print(exception.message);
      print(exception.message);
      print(exception.message);
      print(exception.message);

      showAlertDialog(context, exception.message);

    };



    await FirebaseAuth.instance.verifyPhoneNumber(

      phoneNumber: this.phoneNo,

      codeAutoRetrievalTimeout: autoRetrieve,

      codeSent: smsCodeSent,

      timeout: const Duration(seconds: 20),

      verificationCompleted: verificationSuccess,

      verificationFailed: verificationFailed



    );

  }



  Future<bool> smsCodeDialog(BuildContext context){

    return showDialog(

      context: context,

      barrierDismissible: false,

      builder: (BuildContext context)=> AlertDialog(

        title: Text("Enter SMS code"),

        content: TextField(

          onChanged: (value){

            this.smsCode=value;

          }

        ),

        actions: <Widget>[

          RaisedButton(

            color: Colors.teal,

            child: Text("Done", style: TextStyle(color: Colors.white),),

            onPressed: (){

              FirebaseAuth.instance.currentUser().then((user){

                 if(user!=null){

                    Navigator.pop(context);

                  Navigator.pushReplacement(context, MaterialPageRoute(builder: (BuildContext context)=>

                  Location()

                 

                ));

                 }

                 else{

                   Navigator.pop(context);

                   signIn();

                 }

              });

            },

          )

        ],

      )

      );

  }



  signIn()async{

    final AuthCredential credential= PhoneAuthProvider.getCredential(

      verificationId: verificationId,

      smsCode: smsCode,

    );

    await FirebaseAuth.instance.signInWithCredential(credential).then((user){

      if(user != null){
Navigator.pushReplacement(context, MaterialPageRoute(builder: (BuildContext context)=>

                  Location()));

      }

      

    }).catchError((e){
showAlertDialog(context, "Error is $e");

    });

  }





  @override

  Widget build(BuildContext context) {

    return Scaffold(

      appBar: AppBar(title: Text("Sign In through phone no"),),

      body: ListView(children: <Widget>[

        

        TextField(
          keyboardType: TextInputType.number,

          decoration: InputDecoration(hintText: "Enter phone number"),

          onChanged: (value){

            this.phoneNo= "+91$value";

          },
          

        ),
        SizedBox(height: 30,),

        RaisedButton(

          color: Colors.teal,

          onPressed: verifyNumber,

          child: Text("Verify", style: TextStyle(color: Colors.white),),

        )

      ],),

    );

  }

}
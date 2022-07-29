import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormArray } from '@angular/forms';


@Component({
  selector: 'app-createvm-dialog',
  templateUrl: './createvm-dialog.component.html',
  styleUrls: ['./createvm-dialog.component.css']
})
export class CreatevmDialogComponent implements OnInit {
  genders=['male','female'];
  createVmForm: FormGroup;
  forbiddenUserNames=['geetha','puja'];
  constructor() { }

  ngOnInit() {

    this.createVmForm = new FormGroup({
      'userData': new FormGroup({
          'username':new FormControl(null),
          'email':new FormControl(null),
      }),
      'gender':new FormControl('female'),
      'hobbies':new FormArray([])
    });

    this.createVmForm.setValue({
      'userData':{
        'username':'geetha',
        'email':'geetha@gmail.com'
      },
      'gender':'female',
      'hobbies':[]
    })
  }

  onSubmit(){
    console.log(this.createVmForm);
  }



}

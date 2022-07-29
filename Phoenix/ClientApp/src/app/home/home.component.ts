import { Component, Inject } from '@angular/core';
import { VMService } from '../services/vm.service';
import { HttpClient } from '@angular/common/http';
import { VM } from '../models/vm';
import { Template } from '../models/template';
import { ErrorService } from '../services/error.service';
import {MatSelectModule, MatSelect} from '@angular/material/select';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FormArray } from '@angular/forms';


@
Component({
selector: 'home-component',
templateUrl: './home.component.html',
styleUrls: ['./home.component.css'],
providers: [
  VMService,
  ErrorService
]
})
export class HomeComponent {


  public instances: VM[];
  public templates: Template[];

  profileForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: [''],
    address: this.fb.group({
      street: [''],
      city: [''],
      state: [''],
      zip: ['']
    }),
    aliases: this.fb.array([
      this.fb.control('')
    ])
  })
  displayedColumns: string[] = ['instanceId', 'name', 'instanceType', 'state'];
  constructor(
    http: HttpClient,
    public dialog: MatDialog,
    public vmService : VMService,
    private fb: FormBuilder,
    @Inject('BASE_URL')
    baseUrl: string
    ) {
      this.vmService.getAllVMs().subscribe(result => {
      this.instances = result;
    }, error => console.error(error));

    this.vmService.GetTemplates().subscribe(resultTemplates => {
      this.templates = resultTemplates;
    }, error => console.error(error));


  }

  selectedTemplate = 'option2';

 
  public CreateWindowsAD() {
    this.vmService.CreateWindowsAD().subscribe(result => {
      this.instances = result;
    }, error => console.error(error))
    window.location.reload();

  }

  public confirmStartVM(id: string) {
    this.vmService.StartInstance(id).subscribe(result => {
      this.instances = result;
    }, error => console.error(error))
    window.location.reload();
  }

  public confirmStopVM(id: string) {
    this.vmService.StopInstance(id).subscribe(result => {
      this.instances = result;
    }, error => console.error(error))
    window.location.reload();
  }



  public GetAllVMs() {
    this.vmService.getAllVMs().subscribe(result => {
      this.instances = result;
    }, error => console.error(error))
    window.location.reload();

  }

}




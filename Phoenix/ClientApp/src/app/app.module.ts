import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CreatevmDialogComponent, HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AwsComponent } from './aws/aws.component';
import { VMService } from './services/vm.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MaterialExampleModule} from '../app/material.module';


@NgModule({


 declarations: [
    AwsComponent,
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CreatevmDialogComponent,
      CreatevmDialogComponent
   ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialExampleModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'aws', component: AwsComponent },
    ]),
    BrowserAnimationsModule
  ],


providers: [
   { provide: 'BASE_URL', useFactory: getBaseUrl },
   VMService,
    ],
  bootstrap: [AppComponent,AwsComponent]
})
export class AppModule { }

export function getBaseUrl() {
  return 'https://localhost:44334/';
}

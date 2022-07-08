import { Component, Inject } from '@angular/core';
import { Ec2instanceService } from '../services/ec2instance.service';
import { HttpClient } from '@angular/common/http';
import { Ec2instance } from '../models/ec2instance';
import { ErrorService } from '../services/error.service';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
];

@Component({
selector: 'aws-component',
templateUrl: './aws.component.html',
styleUrls: ['./aws.component.css'],
providers: [
  Ec2instanceService,
  ErrorService
]
})
export class AwsComponent {


  public instances: Ec2instance[];
  constructor(
    http: HttpClient,
    public ec2Service : Ec2instanceService,
    @Inject('BASE_URL')
    baseUrl: string
    ) {
    http.get<Ec2instance[]>('https://localhost:44334/instances').subscribe(result => {
      this.instances = result;
    }, error => console.error(error));
  }

  columns = [
    {
      columnDef: 'position',
      header: 'No.',
      cell: (element: PeriodicElement) => `${element.position}`,
    },
    {
      columnDef: 'name',
      header: 'Name',
      cell: (element: PeriodicElement) => `${element.name}`,
    },
    {
      columnDef: 'weight',
      header: 'Weight',
      cell: (element: PeriodicElement) => `${element.weight}`,
    },
    {
      columnDef: 'symbol',
      header: 'Symbol',
      cell: (element: PeriodicElement) => `${element.symbol}`,
    },
  ];

  dataSource = ELEMENT_DATA;
  displayedColumns = this.columns.map(c => c.columnDef);

  public CreateInstance() {

  }

  public confirmStartVM(id: string) {
    this.ec2Service.StartInstance(id).subscribe(result => {
      this.instances = result;
    }, error => console.error(error))
    window.location.reload();
  }

  public confirmStopVM(id: string) {
    this.ec2Service.StopInstance(id).subscribe(result => {
      this.instances = result;
    }, error => console.error(error))
    window.location.reload();
  }

}

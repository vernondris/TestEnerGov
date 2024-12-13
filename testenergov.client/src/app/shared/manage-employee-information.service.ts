import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from "@angular/common/http";
import { environment } from '../../environments/environment';
import { Employee } from './employee.model';
import { RoleMasterFile } from './role-master-file.model';
import { Observable } from 'rxjs';
import { EmployeeDataToBeSave } from './employee-data-to-be-save.model';

@Injectable({
  providedIn: 'root'
})
export class ManageEmployeeInformationService {

  url: string = environment.apiBaseUrl;
  public employee: Employee[] = [];
  public manager: Employee[] = [];
  public roleMasterFile: RoleMasterFile[] = [];
  public selectedRolesForEmployeeEntry  = new Array();

  constructor(private http: HttpClient) { }

  getEmployeeInformation() {
    this.http.get(this.url + 'GetEmployees')
      .subscribe(
        {
          next: res => {
            this.employee = res as Employee[];
          },
          error: err => { console.log(err) }
        }
    )
  }

  getManagers() {
    this.http.get(this.url + 'GetManagers')
      .subscribe(
        {
          next: res => {
            this.manager = res as Employee[];
          },
          error: err => { console.log(err) }
        }
      )
  }

  getHierarchyBasedOnSelectedManager( id: number) {
    this.http.get(this.url + 'GetHierarchyBasedOnSelectedManager?managerId=' + id)
      .subscribe(
        {
          next: res => {
            this.employee = res as Employee[];
          },
          error: err => { console.log(err) }
        }
      )
  }

  getRoleMasterFile() {
    this.http.get(this.url + 'GetRoleMasterFile')
      .subscribe(
        {
          next: res => {
            this.roleMasterFile = res as RoleMasterFile[];
          },
          error: err => { console.log(err) }
        }
      )
  }

  addEmployee(employeeDataToBeSave: EmployeeDataToBeSave): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    const body = JSON.stringify(employeeDataToBeSave);
    return this.http.post(this.url + 'AddNewEmployees', body, httpOptions)
  }

}

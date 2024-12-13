import { HttpClient } from '@angular/common/http';
import { Component, OnInit, VERSION, ViewChild, ElementRef, Input } from '@angular/core';
import { ManageEmployeeInformationService } from './shared/manage-employee-information.service';
import { FormGroup, FormControl, Validators, FormsModule } from '@angular/forms';
import { EmployeeDataToBeSave } from './shared/employee-data-to-be-save.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  ReportTo = 0
  HideMainPage = false;
  constructor(public service: ManageEmployeeInformationService) { }

  async ngOnInit() {
    this.init();
  }

  async init() {
    await this.service.getEmployeeInformation();
    await this.service.getManagers();
    await this.service.getRoleMasterFile();
  }

  selectManager(e: any) {
    this.service.employee = []
    this.service.getHierarchyBasedOnSelectedManager(e.target.value);
    this.ReportTo = e.target.value;
  }

  selectRoles(e: any) {
    this.service.employee = []
    var index = e.target.value
    this.service.selectedRolesForEmployeeEntry.push(index);
    console.log(JSON.stringify(this.service.selectedRolesForEmployeeEntry));
  }

  async onSubmit(event: any) {
    event.preventDefault();

    const employeeDataToBeSave: EmployeeDataToBeSave = {
      reportingTo: this.ReportTo,
      fname: event.target.fname.value,
      lname: event.target.lname.value,
      roles: this.service.selectedRolesForEmployeeEntry
    };

    this.service.addEmployee(employeeDataToBeSave).subscribe(res => {
      event.target.fname.value = "";
      event.target.lname.value = "";
      this.service.selectedRolesForEmployeeEntry = [];
      this.HideMainPage = false;
      this.init();
    });
  }

  async addEmployee() {
    this.HideMainPage = true;
    await this.service.getRoleMasterFile();
  }

  cancelEmployee() {
    this.HideMainPage = false;
  }
}

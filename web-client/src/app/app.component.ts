import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'demo-web-client';
  public activeKeys: Array<KeyModel>;
  public keyLogs: Array<KeyModel>;
  public selectedValue: string;

  constructor(private http: HttpClient) {

  }

  ngOnInit(): void {
    this.showActiveKeyList();
  }
  showActiveKeyList() {
    let url = 'http://localhost:5000/api/keys';
    this.http.get<Array<KeyModel>>(url).subscribe(
      response => {
        this.activeKeys = response;
        console.log(response);
      },
      err => {
        console.log("Error.")
      }
    );
  }
  showKeyLog(key: string) {
    let url = 'http://localhost:5000/api/keys/'+ key +'/keyLog';
    this.http.get<Array<KeyModel>>(url).subscribe(
      response => {
        this.keyLogs = response;
        console.log(response);
      },
      err => {
        console.log("Error.")
      }
    );
  }

  ngActive(key: string) {
    const result = this.activeKeys.filter(p => p.name.includes(key))[0];
    result.state = true;
    console.log(result);
  }

  ngPassive(key: string): void {
    const result = this.activeKeys.filter(p => p.name.includes(key))[0];
    result.state = false;
    console.log(result);
  }
}

export interface KeyModel {
  name: string;
  state: boolean;
  description: string
}
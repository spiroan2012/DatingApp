import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  validationErrors: string[] = [];

  constructor(private httpService: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error(){
    this.httpService.get(this.baseUrl+'buggy/not-found').subscribe( response =>{
      console.log(response)
    },
    error=>{
      console.log(error);
    });
  }

  get400Error(){
    this.httpService.get(this.baseUrl+'buggy/bad-request').subscribe( response =>{
      console.log(response)
    },
    error=>{
      console.log(error);
    });
  }

  get500Error(){
    this.httpService.get(this.baseUrl+'buggy/server-error').subscribe( response =>{
      console.log(response)
    },
    error=>{
      console.log(error);
    });
  }

  get401Error(){
    this.httpService.get(this.baseUrl+'buggy/auth').subscribe( response =>{
      console.log(response)
    },
    error=>{
      console.log(error);
    });
  }

  get400ValidationError(){
    this.httpService.post(this.baseUrl+'account/register', {}).subscribe( response =>{
      console.log(response)
    },
    error=>{
      console.log(error);
      this.validationErrors = error;
    });
  }

}

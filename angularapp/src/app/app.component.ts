import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  // public shortUrls?: ShortUrls[];

  // constructor(http: HttpClient) {
  //   http.get<ShortUrls[]>('/api/ShortUrls').subscribe(result => {
  //     this.shortUrls = result;
  //   }, error => console.error(error));
  // }

  title = 'angularapp';
}



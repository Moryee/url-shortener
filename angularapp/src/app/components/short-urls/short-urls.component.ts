import { Component } from '@angular/core';
import { ShortUrl } from 'src/app/models/short-url.model';
import { ShortUrlsService } from 'src/app/services/short-urls.service';

@Component({
  selector: 'app-short-urls',
  templateUrl: './short-urls.component.html',
  styleUrls: ['./short-urls.component.css']
})
export class ShortUrlsComponent {
  constructor(private shortUrlsService: ShortUrlsService) { }

  shortUrls: ShortUrl[] = [];

  ngOnInit(): void { 
    this.shortUrlsService.getShortUrls().subscribe(response => {
      this.shortUrls = response;
      console.log(response);
      
    });
  }
}

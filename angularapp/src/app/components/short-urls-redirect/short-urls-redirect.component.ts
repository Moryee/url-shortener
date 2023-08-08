import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ShortUrlsService } from 'src/app/services/short-urls.service';

@Component({
  selector: 'app-short-urls-redirect',
  templateUrl: './short-urls-redirect.component.html',
  styleUrls: ['./short-urls-redirect.component.css']
})
export class ShortUrlsRedirectComponent {

  constructor(
    private route: ActivatedRoute,
    private shortUrlsService: ShortUrlsService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const shortUrl = params.get('shortUrl');

      if (shortUrl) {
        this.shortUrlsService.getShortUrlByShortUrl(shortUrl).subscribe(response => {
          if (response) {
            console.log(response);
            window.location.href = response.url;
          }
          else {
            console.log('Short url not found');
            this.router.navigate(['/short-urls/create']);
          }
        });
      }
      else {
        console.log('Param shortUrl not found');
        this.router.navigate(['/short-urls/create']);
      }
    })
  }
}

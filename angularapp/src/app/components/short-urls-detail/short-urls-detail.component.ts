import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ShortUrl } from 'src/app/models/short-url.model';
import { ShortUrlsService } from 'src/app/services/short-urls.service';
import { FormBuilder, Validators, AsyncValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { ShortUrlUpdate } from 'src/app/models/short-url-update.model';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-short-urls-detail',
  templateUrl: './short-urls-detail.component.html',
  styleUrls: ['./short-urls-detail.component.css']
})
export class ShortUrlsDetailComponent {
  shortUrl: ShortUrl | undefined;
  
  shortUrlForm = this.fb.group({
    url: ['', Validators.required],
    shortenedUrl: ['', Validators.compose([Validators.required, Validators.pattern('^[a-zA-Z0-9]*$')])],
  });

  constructor(
    private route: ActivatedRoute,
    private shortUrlsService: ShortUrlsService,
    private fb: FormBuilder,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');

      if (id) {
        this.shortUrlsService.getShortUrl(id).subscribe(response => {
          this.fillForm(response);
        });
      }
    })

    this.shortUrlForm.controls.shortenedUrl.setAsyncValidators(this.createUniqueShortUrlValidator())
  }

  createUniqueShortUrlValidator() {
    return (control: AbstractControl) => {
      let value = this.shortUrlForm.controls['shortenedUrl'].value
      if (value && value.length > 0) {
        this.shortUrlsService.getShortUrlByShortUrl(value).subscribe(response => {
          console.log(response);
          if (response) {
            this.shortUrlForm.controls['shortenedUrl'].setErrors({ 'unique_error': 'Short url must be unique' });
          }
        });
      }
      return of(null);
    }
  }


  onSubmit(): void {
    let shortUrl = this.getCleanedFormData();
    if (shortUrl && this.shortUrl) {
      this.shortUrlsService.updateShortUrl(this.shortUrl.id, shortUrl).subscribe(response => {
        this.fillForm(response);
      });
    }
  }

  deleteShortUrl(): void {
    if (this.shortUrl) {
      this.shortUrlsService.deleteShortUrl(this.shortUrl.id).subscribe(response => {
        this.router.navigate(['/short-urls']);
      });
    }
  }

  // helper methods

  getCleanedFormData(): ShortUrlUpdate {
    let shortUrl: ShortUrlUpdate = {
      url: this.shortUrlForm.value.url as string,
      shortenedUrl: this.shortUrlForm.value.shortenedUrl as string,
    };
    return shortUrl;
  }

  fillForm(shortUrl: ShortUrl): void {
    this.shortUrl = shortUrl;
    if (shortUrl) {
      this.shortUrlForm.patchValue({
        url: shortUrl.url,
        shortenedUrl: shortUrl.shortenedUrl,
      });
    }
  }

  clearForm(): void {
    if (this.shortUrl) {
      this.fillForm(this.shortUrl);
    }
    // this.shortUrlForm.patchValue({
    //   url: '',
    //   shortenedUrl: '',
    // });
  }
}

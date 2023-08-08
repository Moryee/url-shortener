import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShortUrlsService } from 'src/app/services/short-urls.service';
import { FormBuilder, Validators } from '@angular/forms';
import { ShortUrlCreate } from 'src/app/models/short-url-create.model';
import { AsyncValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-short-urls-create',
  templateUrl: './short-urls-create.component.html',
  styleUrls: ['./short-urls-create.component.css']
})
export class ShortUrlsCreateComponent {
  shortUrlForm = this.fb.group({
    url: ['', Validators.required],
    shortenedUrl: ['', Validators.required],
    userId: [''],
  });

  constructor(
    private route: ActivatedRoute,
    private shortUrlsService: ShortUrlsService,
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.shortUrlForm.controls.shortenedUrl.setAsyncValidators(this.createUniqueShortUrlValidator())
  }

  createUniqueShortUrlValidator(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors> => {
      let value = this.shortUrlForm.controls['shortenedUrl'].value
      if (value && value.length > 0) {
        this.shortUrlsService.getShortUrlByShortUrl(value).subscribe(response => {
          console.log(response);
          if (response) {
            this.shortUrlForm.controls['shortenedUrl'].setErrors({ 'unique_error': 'Short url must be unique' });
            return
          }
          else {
            return of(Validators.nullValidator)
          }
        });
      }
      return of(Validators.nullValidator)
    }
  }

  onSubmit(): void {
    let shortUrl = this.getCleanedFormData();
    if (shortUrl) {
      this.shortUrlsService.createShortUrl(shortUrl).subscribe(response => {
        this.clearForm();
      });
    }
  }

  getCleanedFormData(): ShortUrlCreate {
    let shortUrl: ShortUrlCreate = {
      url: this.shortUrlForm.value.url as string,
      shortenedUrl: this.shortUrlForm.value.shortenedUrl as string,
      userId: 'e71e5a24-3515-11ee-be56-0242ac120002',
    };
    return shortUrl;
  }

  clearForm(): void {
    this.shortUrlForm.patchValue({
      url: '',
      shortenedUrl: '',
      userId: '',
    });
  }
}

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortUrlsRedirectComponent } from './short-urls-redirect.component';

describe('ShortUrlsRedirectComponent', () => {
  let component: ShortUrlsRedirectComponent;
  let fixture: ComponentFixture<ShortUrlsRedirectComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShortUrlsRedirectComponent]
    });
    fixture = TestBed.createComponent(ShortUrlsRedirectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

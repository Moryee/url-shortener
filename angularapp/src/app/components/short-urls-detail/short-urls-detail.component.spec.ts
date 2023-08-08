import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortUrlsDetailComponent } from './short-urls-detail.component';

describe('ShortUrlsDetailComponent', () => {
  let component: ShortUrlsDetailComponent;
  let fixture: ComponentFixture<ShortUrlsDetailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShortUrlsDetailComponent]
    });
    fixture = TestBed.createComponent(ShortUrlsDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortUrlsCreateComponent } from './short-urls-create.component';

describe('ShortUrlsCreateComponent', () => {
  let component: ShortUrlsCreateComponent;
  let fixture: ComponentFixture<ShortUrlsCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShortUrlsCreateComponent]
    });
    fixture = TestBed.createComponent(ShortUrlsCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

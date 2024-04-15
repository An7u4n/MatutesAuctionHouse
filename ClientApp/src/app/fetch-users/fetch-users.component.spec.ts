import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchUsersComponent } from './fetch-users.component';

describe('FetchUsersComponent', () => {
  let component: FetchUsersComponent;
  let fixture: ComponentFixture<FetchUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FetchUsersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FetchUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

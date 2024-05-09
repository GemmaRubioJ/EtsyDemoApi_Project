import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserExistDialogComponent } from './user-exist-dialog.component';

describe('UserExistDialogComponent', () => {
  let component: UserExistDialogComponent;
  let fixture: ComponentFixture<UserExistDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserExistDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserExistDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

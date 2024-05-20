import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserExistsDialogComponent } from './user-exist-dialog.component';

describe('UserExistDialogComponent', () => {
  let component: UserExistsDialogComponent;
  let fixture: ComponentFixture<UserExistsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserExistsDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserExistsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

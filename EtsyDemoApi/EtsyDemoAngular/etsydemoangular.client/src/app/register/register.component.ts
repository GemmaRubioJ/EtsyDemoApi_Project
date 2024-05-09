import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { UserService } from '../Services/user.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { SuccessDialogComponent } from '../success-dialog-component/success-dialog-component.component';
import { UserExistsDialogComponent } from '../user-exist-dialog/user-exist-dialog.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup = this.fb.group({}); // Inicialización básica

  constructor(private fb: FormBuilder,
              private userService: UserService,
              private router: Router,
              private dialog: MatDialog) { }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required], // Campo para confirmar la contraseña
      name: this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
      }),
      address: this.fb.group({
        city: ['', Validators.required],
        street: ['', Validators.required],
        number: ['', Validators.required],
        zipcode: ['', Validators.required]
      }),
      phone: ['', Validators.required]
    }, { validator: this.passwordMatchValidator }); // Validador para coincidencia de contraseñas
    console.log('Form initialized', this.registerForm.value);
  }

  passwordMatchValidator(frm: FormGroup): { [key: string]: boolean } | null {
    const pass = frm.get('password');
    const confirmPass = frm.get('confirmPassword');
    return pass && confirmPass && pass.value === confirmPass.value ? null : { 'mismatch': true };
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      this.userService.registerUser(this.registerForm.value).subscribe({
        next: (response) => {
          // Check the response content here, not just rely on the next callback
          if (response.status === 2 && response.message === "El usuario ya existe") {
            this.dialog.open(UserExistsDialogComponent, {
              data: { message: "Usuario ya existe" }
            });
          } else {
            this.dialog.open(SuccessDialogComponent, {
              data: { message: "Te registraste correctamente" }
            });
            this.router.navigate(['/']);
          }
        },
        error: (errorResponse) => {
          console.error('Error en el registro', errorResponse);
          // Handle unexpected errors
          console.error('Unexpected error:', errorResponse);
        }
      });
    } else {
      console.log('Form is not valid', this.registerForm);
    }
  }
}

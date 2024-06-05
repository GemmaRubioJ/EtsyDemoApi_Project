import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, ValidationErrors } from '@angular/forms';
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

  registerForm: FormGroup =  this.fb.group({});

  constructor(private fb: FormBuilder,
              private userService: UserService,
              private router: Router,
              private dialog: MatDialog) { }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
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
    }, { validators: this.passwordMatchValidator });

    this.monitorFormChanges();
  }


  monitorFormChanges(): void {
    this.registerForm.valueChanges.subscribe(value => {
      console.log('Form Value Changed:', value);
      console.log('Form Status:', this.registerForm.status);
    });

    // Individual field monitoring
    this.registerForm.get('password')?.valueChanges.subscribe(() => {
      this.registerForm.updateValueAndValidity();
    });

    this.registerForm.get('confirmPassword')?.valueChanges.subscribe(() => {
      this.registerForm.updateValueAndValidity();
    });
  }

   passwordMatchValidator(frm: FormGroup): ValidationErrors | null {
      const pass = frm.get('password')?.value;
     const confirmPass = frm.get('confirmPassword')?.value;
     console.log('Validator called: ', pass, confirmPass);
      return pass === confirmPass ? null : { mismatch: true };
    }

  onSubmit(): void {
    // Muestra siempre los estados de los formularios y los errores para diagnóstico
    console.log('onSubmit called');
    console.log('Form Value:', this.registerForm.value);
    console.log('Form Valid:', this.registerForm.valid);
    console.log('Form Errors:', this.registerForm.errors);
    console.log('Password Errors:', this.registerForm.get('password')?.errors);
    console.log('Confirm Password Errors:', this.registerForm.get('confirmPassword')?.errors);

    if (!this.registerForm.valid) {
      console.log('Form is not valid', this.registerForm);
      return;  // Termina la ejecución si el formulario no es válido
    }

    // Si el formulario es válido, procede con la registración
    this.userService.registerUser(this.registerForm.value).subscribe({
      next: (response) => {
        if (response.status === 2 && response.message === "El usuario ya existe") {
          this.dialog.open(UserExistsDialogComponent, {
            data: { message: "Este Usuario ya existe" }
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
      }
    });
  }
}

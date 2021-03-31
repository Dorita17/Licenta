import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errors: string[];
  activityOptions = [
    {name: 'Option 1', value: 1.3},
    {name: 'Option 2', value: 1.6},
    {name: 'Option 3', value: 1.8},
    {name: 'Option 4', value: 2},
    {name: 'Option 5', value: 2.3},

  ];

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      displayName: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\.)+[\\w-]{2,4}$')]],
      password: [null, [Validators.required]],
      gender: [null, [Validators.required]],
      weight: [null, [Validators.required]],
      height: [null, [Validators.required]],
      dateOfBirth: [null, [Validators.required]],
      activityCost: [null, Validators.required]
    });
  }

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/menus');
    }, error => {
      console.log(error);
      this.errors = error.errors;
    });
  }

}

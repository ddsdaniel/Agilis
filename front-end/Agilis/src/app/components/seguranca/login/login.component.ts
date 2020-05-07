import { Component, OnInit } from '@angular/core';
import { LoginDados } from 'src/app/models/seguranca/login-dados';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  login: LoginDados = {
    email: '',
    senha: ''
  };

  constructor() { }

  ngOnInit() {
  }

  autenticar() {
    alert(this.login.email + ' ' + this.login.senha);
  }
}

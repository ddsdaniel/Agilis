import { Component, OnInit } from '@angular/core';
import { TituloService } from 'src/app/services/titulo.service';

@Component({
  selector: 'app-bem-vindo',
  templateUrl: './bem-vindo.component.html',
  styleUrls: ['./bem-vindo.component.scss']
})
export class BemVindoComponent implements OnInit {

  constructor(
    private tituloService: TituloService,
  ) { }

  ngOnInit() {
    this.tituloService.definir('Bem-vindo');
  }

}

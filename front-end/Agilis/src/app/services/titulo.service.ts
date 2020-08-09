import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TituloService {

  constructor(
    private titleService: Title,
  ) { }

  setTitulo(titulo: string) {
    this.titleService.setTitle(`${titulo} | ${environment.appName}`);
  }
}

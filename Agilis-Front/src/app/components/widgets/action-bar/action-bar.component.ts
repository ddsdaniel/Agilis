import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AutenticacaoService } from 'src/app/services/autenticacao.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { ProcessandoService } from 'src/app/services/processando.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-action-bar',
  templateUrl: './action-bar.component.html',
  styleUrls: ['./action-bar.component.scss']
})
export class ActionBarComponent implements OnInit {

  query: string;
  modo = 'lista';
  nomeAplicacao: string;

  @ViewChild('busca') buscaElement: ElementRef;

  @Input() titulo: string;
  @Input() botaoFiltro = false;
  @Input() botaoPesquisa = false;

  @Output() pesquisaEvent = new EventEmitter<string>();

  pesquisar() {
    this.pesquisaEvent.emit(this.query);
  }

  constructor(
    public processandoService: ProcessandoService,
    public router: Router,
    private localStorageService: LocalStorageService,
    private autenticacaoService: AutenticacaoService,
  ) {
    this.nomeAplicacao = environment.nomeAplicacao;
  }


  ngOnInit(): void {
  }

  sair() {
    this.autenticacaoService.limparUsuarioLogado();
    this.localStorageService.clear();
    this.router.navigateByUrl('bem-vindo');
  }

  abrirInstagram() {
    window.open('https://www.instagram.com/appAgilis');
  }

  fecharPesquisa() {
    this.query = '';
    this.modo = 'lista';
    this.pesquisaEvent.emit('');
  }

  abrirPesquisa(query: string = '') {
    this.query = query;
    this.modo = 'pesquisa';
    setTimeout(() => {
      this.buscaElement.nativeElement.focus();
    }, 5);
  }

  nomeUsuario() {
    return `${this.autenticacaoService.usuarioLogado.usuario.nome} ${this.autenticacaoService.usuarioLogado.usuario.sobrenome}`;
  }

  emailUsuario() {
    return this.autenticacaoService.usuarioLogado.usuario.email;
  }

}

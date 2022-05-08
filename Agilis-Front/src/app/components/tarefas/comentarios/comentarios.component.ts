import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Comentario } from 'src/app/models/tarefas/comentario';
import { AutenticacaoService } from 'src/app/services/autenticacao.service';

@Component({
  selector: 'app-comentarios',
  templateUrl: './comentarios.component.html',
  styleUrls: ['./comentarios.component.scss']
})
export class ComentariosComponent implements OnInit {

  novoComentario: Comentario;
  @Input() comentarios: Comentario[] = [];
  @Output() comentariosChange = new EventEmitter<Comentario[]>();

  constructor(
    private autenticacaoSerivce: AutenticacaoService,
  ) { }

  ngOnInit(): void {
    this.inicializarNovoComentario();
  }

  private inicializarNovoComentario() {
    this.novoComentario = {
      autor: this.autenticacaoSerivce.usuarioLogado.usuario,
      dataHora: new Date(),
      descricao: ''
    };
  }

  adicionar() {
    if (this.novoComentario.descricao) {
      this.novoComentario.dataHora = new Date();
      const clone = Object.assign({}, this.novoComentario);
      this.comentarios.push(clone);
      this.inicializarNovoComentario();
    }
  }

}

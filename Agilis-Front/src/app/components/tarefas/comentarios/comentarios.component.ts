import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Comentario } from 'src/app/models/tarefas/comentario';
import { UsuarioApiService } from 'src/app/services/apis/usuario-api.service';
import { AutenticacaoService } from 'src/app/services/autenticacao.service';

@Component({
  selector: 'app-comentarios',
  templateUrl: './comentarios.component.html',
  styleUrls: ['./comentarios.component.scss']
})
export class ComentariosComponent implements OnInit {

  mencoes: string[] = [];
  novoComentario: Comentario;

  @Input() comentarios: Comentario[] = [];
  @Output() comentariosChange = new EventEmitter<Comentario[]>();

  constructor(
    private autenticacaoSerivce: AutenticacaoService,
    private usuarioApiService: UsuarioApiService,
    private snackBar: MatSnackBar,
  ) { }

  ngOnInit(): void {
    this.inicializarNovoComentario();
    this.usuarioApiService.obterTodos()
      .subscribe({
        next: usuarios => this.mencoes = usuarios.map(u => `${u.nome} ${u.sobrenome}`)
      });
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
      this.comentariosChange.emit(this.comentarios);
      this.inicializarNovoComentario();
    }
  }

  excluir(indice: number) {

    const removido = this.comentarios[indice];
    this.comentarios.removeAt(indice);
    this.comentariosChange.emit(this.comentarios);

    const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

    snackBarRef.onAction().subscribe(() => {

      this.comentarios.insert(indice, removido);
      this.comentariosChange.emit(this.comentarios);

    });

  }

}

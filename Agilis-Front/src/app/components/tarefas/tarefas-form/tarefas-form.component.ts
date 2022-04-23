import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';
import { switchMap } from 'rxjs/operators';
import { constantes } from 'src/app/consts/constantes';
import { OperacaoFormCrud } from 'src/app/enums/operacao-form-crud.enum';
import { RegraUsuario } from 'src/app/enums/regra-usuario.enum';
import { TipoTarefa, TipoTarefaLabel } from 'src/app/enums/tipo-tarefa.enum';
import { UsuarioConsulta } from 'src/app/models/seguranca/usuario-consulta';
import { Tag } from 'src/app/models/tags/tag';
import { Tarefa } from 'src/app/models/tarefas/tarefa';
import { FeatureApiService } from 'src/app/services/apis/produtos/feature-api.service';
import { TagApiService } from 'src/app/services/apis/tag-api.service';
import { TarefaApiService } from 'src/app/services/apis/tarefa-api.service';
import { UsuarioApiService } from 'src/app/services/apis/usuario-api.service';
import { ComparadorService } from 'src/app/services/comparador.service';
import { TituloService } from 'src/app/services/titulo.service';

import { CrudFormComponent } from '../../crud/crud-form-component';

@Component({
  selector: 'app-tarefas-form',
  templateUrl: './tarefas-form.component.html',
  styleUrls: ['./tarefas-form.component.scss']
})
export class TarefasFormComponent extends CrudFormComponent<Tarefa> implements OnInit {

  usuarios: UsuarioConsulta[];
  tipos = Object.keys(TipoTarefa);
  tagValue = '';
  tagsFiltradas: Tag[];
  todasAsTags: Tag[] = [];
  tagSeparatorKeysCodes: number[] = [ENTER, COMMA];
  @ViewChild('tagInput') tagInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto') matAutocomplete: MatAutocomplete;

  constructor(
    private featureApiService: FeatureApiService,
    private usuarioApiService: UsuarioApiService,
    private tagApiService: TagApiService,
    router: Router,
    tarefaApiService: TarefaApiService,
    snackBar: MatSnackBar,
    tituloService: TituloService,
    protected activatedRoute: ActivatedRoute,
    public comparadorService: ComparadorService,
  ) {
    super(router, tarefaApiService, snackBar, activatedRoute, 'tarefas');
    tituloService.definir('Cadastro da Tarefa');
  }

  carregarDependencias(): Observable<void> {
    return this.obterUsuarios()
      .pipe(
        switchMap(_ => this.identificarFeature()),
        switchMap(_ => this.obterTags())
      );
  }

  adicionarTag(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our tag
    if ((value || '').trim()) {

      const tag: Tag = {
        id: constantes.newGuid,
        nome: value.trim(),
        tarefas: [],
      };

      this.tagApiService.adicionar(tag)
        .subscribe({
          next: id => {
            tag.id = id;
            this.entidade.tags.push(tag);
          }
        });

    }

    // Reset the input value
    if (input) {
      input.value = '';
    }

    this.tagValue = '';
  }

  selecionouTag(event: MatAutocompleteSelectedEvent): void {
    const tag: Tag = {
      id: constantes.newGuid,
      nome: event.option.viewValue,
      tarefas: [],
    };
    this.entidade.tags.push(tag);
    this.tagInput.nativeElement.value = '';
    this.tagValue = '';
  }

  removerTag(tag: Tag): void {
    const index = this.entidade.tags.findIndex(t => t.id === tag.id);

    if (index >= 0) {
      this.entidade.tags.splice(index, 1);
    }
  }

  filtrarTagControl() {
    const filterValue = this.tagValue.toLowerCase();
    this.tagsFiltradas = this.todasAsTags.filter(tag => tag.nome.toLowerCase().indexOf(filterValue) === 0);
  }

  obterTags(): Observable<any> {
    return this.tagApiService.obterTodos()
      .pipe(
        tap(tags => this.todasAsTags = tags)
      );
  }

  obterUsuarios(): Observable<any> {
    return this.usuarioApiService.obterTodos()
      .pipe(
        tap(usuarios => this.usuarios = usuarios)
      );
  }

  private identificarFeature(): Observable<any> {
    return this.activatedRoute.queryParams
      .pipe(
        tap(params => {
          if (params.featureId) {
            this.featureApiService.obterUm(params.featureId)
              .subscribe({
                next: feature => {

                  if (super.operacao === OperacaoFormCrud.adicionando) {
                    this.entidade.feature = feature;
                  }
                  this.rotaPesquisa = `/produtos/${feature.epico.produto.id}/backlog`;
                }
              });
          }
        }
        ));
  }

  sugerirNovo(): void {
    this.entidade = {
      id: constantes.newGuid,
      titulo: '',
      descricao: '',
      horasPrevistas: '00:00',
      horasRealizadas: '00:00',
      tags: [],
      feature: {
        id: constantes.newGuid,
        nome: '',
        tarefas: [],
        epico: {
          id: constantes.newGuid,
          nome: '',
          features: [],
          produto: {
            id: constantes.newGuid,
            nome: '',
            descricao: '',
            urlRepositorio: '',
            epicos: []
          }
        }
      },
      relator: {
        id: constantes.newGuid,
        nome: '',
        sobrenome: '',
        ativo: true,
        email: '',
        regra: RegraUsuario.Usuario,
      },
      tipo: TipoTarefa.Novidade,
    };
  }

  obterLabelTipo(tipo: TipoTarefa): string {
    return TipoTarefaLabel.get(tipo);
  }

}

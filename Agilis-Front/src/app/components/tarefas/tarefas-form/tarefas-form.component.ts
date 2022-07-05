import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';
import { switchMap } from 'rxjs/operators';
import { constantes } from 'src/app/consts/constantes';
import { SituacaoTarefa, SituacaoTarefaLabel } from 'src/app/enums/situacao-tarefa.enum';
import { TipoAnexo } from 'src/app/enums/tipo-anexo.enum';
import { TipoTarefa, TipoTarefaLabel } from 'src/app/enums/tipo-tarefa.enum';
import { AnexoFk } from 'src/app/models/anexo-fk';
import { BottomSheetItem } from 'src/app/models/bottom-sheet-item';
import { Cliente } from 'src/app/models/cliente';
import { Feature } from 'src/app/models/produtos/feature';
import { UsuarioConsulta } from 'src/app/models/seguranca/usuario-consulta';
import { Release } from 'src/app/models/release';
import { Sprint } from 'src/app/models/sprint';
import { CheckList } from 'src/app/models/tarefas/check-list';
import { Tarefa } from 'src/app/models/tarefas/tarefa';
import { AnexoApiService } from 'src/app/services/apis/anexo-api.service';
import { ClienteApiService } from 'src/app/services/apis/cliente-api.service';
import { FeatureApiService } from 'src/app/services/apis/produtos/feature-api.service';
import { ReleaseApiService } from 'src/app/services/apis/release-api.service';
import { SprintApiService } from 'src/app/services/apis/sprint-api.service';
import { TarefaApiService } from 'src/app/services/apis/tarefa-api.service';
import { UsuarioApiService } from 'src/app/services/apis/usuario-api.service';
import { BottomSheetService } from 'src/app/services/bottom-sheet.service';
import { ComparadorService } from 'src/app/services/comparador.service';
import { TituloService } from 'src/app/services/titulo.service';
import { environment } from 'src/environments/environment';

import { CrudFormComponent } from '../../crud/crud-form-component';
import { BottomSheetComponent } from '../../widgets/bottom-sheet/bottom-sheet.component';
import { TagsComponent } from '../tags/tags.component';

@Component({
  selector: 'app-tarefas-form',
  templateUrl: './tarefas-form.component.html',
  styleUrls: ['./tarefas-form.component.scss']
})
export class TarefasFormComponent extends CrudFormComponent<Tarefa> implements OnInit {

  @ViewChild('tags', { static: true }) tagsViewChild: TagsComponent;
  usuarios: UsuarioConsulta[];
  tipos = Object.keys(TipoTarefa);
  situacoes = Object.keys(SituacaoTarefa);
  features: Feature[] = [];
  clientes: Cliente[] = [];
  releases: Release[] = [];
  sprints: Sprint[] = [];

  constructor(
    private featureApiService: FeatureApiService,
    private usuarioApiService: UsuarioApiService,
    private clienteApiService: ClienteApiService,
    private releaseApiService: ReleaseApiService,
    private sprintApiService: SprintApiService,
    private anexoApiService: AnexoApiService,
    private bottomSheetService: BottomSheetService,
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
        switchMap(_ => this.obterFeatures()),
        switchMap(_ => this.obterClientes()),
        switchMap(_ => this.obterReleases()),
        switchMap(_ => this.obterSprints()),
        switchMap(_ => this.tagsViewChild.obterTags())
      );
  }

  obterReleases(): Observable<any> {
    return this.releaseApiService.obterTodos()
      .pipe(
        tap(releases => this.releases = releases)
      );
  }

  obterSprints(): Observable<any> {
    return this.sprintApiService.obterTodos()
      .pipe(
        tap(sprints => this.sprints = sprints)
      );
  }

  obterClientes(): Observable<any> {
    return this.clienteApiService.obterTodos()
      .pipe(
        tap(clientes => this.clientes = clientes)
      );
  }

  private obterUsuarios(): Observable<any> {
    return this.usuarioApiService.obterTodos()
      .pipe(
        tap(usuarios => this.usuarios = usuarios)
      );
  }

  private obterFeatures(): Observable<any> {
    return this.featureApiService.obterTodos()
      .pipe(
        tap(features => this.features = features)
      );
  }

  sugerirNovo(): void {

    this.entidade = {
      id: constantes.newGuid,
      titulo: '',
      checkLists: [],
      descricao: '',
      horasPrevistas: '00:00',
      horasRealizadas: '00:00',
      tags: [],
      feature: null,
      relator: null,
      tipo: TipoTarefa.Novidade,
      cliente: null,
      valor: 0,
      urlTicketSAC: '',
      comentarios: [],
      anexos: [],
      release: null,
      sprint: null,
      situacao: SituacaoTarefa.AFazer,
      solucao: '',
      branches: ''
    };
  }

  obterLabelSituacao(situacao: SituacaoTarefa): string {
    return SituacaoTarefaLabel.get(situacao);
  }

  obterLabelTipo(tipo: TipoTarefa): string {
    return TipoTarefaLabel.get(tipo);
  }

  adicionar() {

    const itens: BottomSheetItem[] = [
      {
        codigo: 'check-list',
        titulo: 'Check-List',
        subTitulo: 'Adicionar um check-list',
        icone: 'check_box'
      },
    ];

    this.bottomSheetService.abrir(itens, BottomSheetComponent)
      .subscribe(codigo => {
        if (codigo) {
          switch (codigo) {
            case 'check-list':
              this.adicionarCheckList();
              break;
          }
        }
      });
  }

  adicionarCheckList() {

    const ordem = this.entidade.checkLists && this.entidade.checkLists.length > 0
      ? Math.max.apply(null, this.entidade.checkLists.map(x => x.ordem)) + 1
      : 1;

    const checkList: CheckList = {
      id: constantes.newGuid,
      nome: 'Novo check-list',
      itens: [],
      ordem,
      tarefa: null
    };

    this.entidade.checkLists.push(checkList);
  }

  navegarUrlTicketSAC() {
    if (this.entidade.urlTicketSAC) {
      window.open(this.entidade.urlTicketSAC, '_blank');
    }
  }

  download(anexoFk: AnexoFk): void {
    this.anexoApiService.obterUm(anexoFk.anexoId)
      .subscribe({
        next: anexo => {
          if (anexo.tipo === TipoAnexo.Link) {
            window.open(anexo.conteudo, '_blank');
          } else {
            const url = `${environment.apiUrl}/anexo/${anexoFk.anexoId}/download`;
            window.open(url, '_blank');
          }
        }
      });
  }
}

import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BottomSheetItem } from 'src/app/models/bottom-sheet-item';
import { Epico } from 'src/app/models/produtos/epico';
import { Produto } from 'src/app/models/produtos/produto';
import { EpicoApiService } from 'src/app/services/apis/produtos/epico-api.service';
import { ProdutoApiService } from 'src/app/services/apis/produtos/produto-api.service';
import { BottomSheetService } from 'src/app/services/bottom-sheet.service';
import { TituloService } from 'src/app/services/titulo.service';
import { BottomSheetComponent } from '../../widgets/bottom-sheet/bottom-sheet.component';

@Component({
  selector: 'app-product-backlog',
  templateUrl: './product-backlog.component.html',
  styleUrls: ['./product-backlog.component.scss']
})
export class ProductBacklogComponent implements OnInit {

  produto: Produto;
  titulo = 'Product Backlog';

  constructor(
    private activatedRoute: ActivatedRoute,
    private produtoApiService: ProdutoApiService,
    private tituloService: TituloService,
    private router: Router,
    private bottomSheetService: BottomSheetService,
    private epicoApiService: EpicoApiService,
    private snackBar: MatSnackBar,
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      () => {

        const id = this.activatedRoute.snapshot.paramMap.get('id');

        this.produtoApiService.obterUm(id)
          .subscribe({
            next: produto => {
              this.produto = produto;
              this.titulo = `${produto.nome} - Product Backlog`;
              this.tituloService.definir(this.titulo);
            }
          });
      });
  }

  openBottomSheetEpicos(id: string, index: number): void {

    const itens: BottomSheetItem[] = [
      {
        codigo: 'add-feature',
        titulo: 'Adicionar Feature',
        subTitulo: 'Abre uma nova tela para adicionar uma nova feature',
        icone: 'add'
      },
      {
        codigo: 'editar',
        titulo: 'Editar',
        subTitulo: 'Abre uma nova tela para edição',
        icone: 'edit'
      },
      {
        codigo: 'excluir',
        titulo: 'Excluir',
        subTitulo: 'Exclui o épico',
        icone: 'clear',
        cor: '#FF0000'
      }
    ];

    this.bottomSheetService.abrir(itens, BottomSheetComponent)
      .subscribe(codigo => {
        if (codigo) {
          switch (codigo) {
            case 'add-feature':
              this.navegarParaFeature('/produtos/features/new', this.produto.epicos[index]);
              break;
            case 'editar':
              this.navegarParaEdico(`/produtos/epicos/${id}`);
              break;
            case 'excluir':
              this.excluirEpico(index);
              break;
          }
        }
      });
  }

  excluirEpico(index: number) {

    const epico = this.produto.epicos[index];

    this.epicoApiService.excluir(epico.id)
      .subscribe(
        () => {

          this.produto.epicos.removeAt(index);

          const snackBarRef = this.snackBar.open('Excluído', 'Desfazer');

          snackBarRef.onAction().subscribe(() => {

            const clone = Object.assign({}, epico);

            clone.produto = this.produto;
            this.epicoApiService.adicionar(clone)
              .subscribe(
                () => {
                  this.produto.epicos.insert(index, clone);
                },
                (error: HttpErrorResponse) => this.snackBar.open(error.message)
              );

          });
        },
        (error: HttpErrorResponse) => this.snackBar.open(error.message)
      );
  }

  adicionarEpico() {
    this.navegarParaEdico('/produtos/epicos/new');
  }

  private navegarParaEdico(rota: string) {
    const params = {
      queryParams: {
        produtoId: this.produto.id
      }
    };

    this.router.navigate([rota], params);
  }

  private navegarParaFeature(rota: string, epico: Epico) {
    const params = {
      queryParams: {
        produtoId: this.produto.id,
        epicoId: epico.id,
      }
    };

    this.router.navigate([rota], params);
  }
}

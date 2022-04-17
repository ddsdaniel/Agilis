import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BottomSheetItem } from 'src/app/models/bottom-sheet-item';
import { Produto } from 'src/app/models/produtos/produto';
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
            case 'editar':
              this.navegarParaEdico(`/produtos/epicos/${id}`);
              //super.editar(id);
              break;
            case 'excluir':
              //super.excluir(index);
              break;
          }
        }
      });
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
}

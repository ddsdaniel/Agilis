import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { constantes } from 'src/app/consts/constantes';
import { AnexoFk } from 'src/app/models/anexo-fk';
import { Anexo } from 'src/app/models/anexo';
import { AnexoApiService } from 'src/app/services/apis/anexo-api.service';
import { TipoAnexo } from 'src/app/enums/tipo-anexo.enum';

@Component({
  selector: 'app-anexos',
  templateUrl: './anexos.component.html',
  styleUrls: ['./anexos.component.scss']
})
export class AnexosComponent {

  novoAnexo: AnexoFk;
  @Input() anexos: AnexoFk[] = [];
  @Output() anexosChange = new EventEmitter<AnexoFk[]>();
  @Output() downloadAnexo = new EventEmitter<AnexoFk>();

  constructor(
    private snackBar: MatSnackBar,
    private anexoApiService: AnexoApiService,
  ) { }

  excluir(indice: number) {
    const anexoRemovido = this.anexos[indice];

    this.anexoApiService.excluir(anexoRemovido.anexoId)
      .subscribe({
        next: _ => {
          this.anexos.removeAt(indice);
          this.anexosChange.emit(this.anexos);
          this.snackBar.open('Excluído');
        }
      });

  }

  inputFileChange(event: any) {
    this.selecionouArquivo(event.target.files[0]);
  }

  selecionouArquivo(file: File) {

    const MEGAS = 10;
    const MAX_SIZE = MEGAS * 1024 * 1024;

    if (file.size > MAX_SIZE) {
      this.snackBar.open(`Tamanho máximo do arquivo: ${MEGAS} MB`);

    } else {
      const reader = new FileReader();

      reader.onload = () => {

        const anexo: Anexo = {
          id: constantes.newGuid,
          conteudo: reader.result.valueOf().toString(),
          nome: file.name,
          tipo: TipoAnexo.Arquivo// será corrigido no back-end
        };

        this.upload(anexo, file);

      };
      reader.onerror = () => {
        this.snackBar.open('Erro ao carregar o anexo');
      };
      reader.readAsDataURL(file);
    }
  }

  upload(anexo: Anexo, file: File) {
    this.anexoApiService.adicionar(anexo)
      .subscribe({
        next: id => this.anexar(id, file)
      });
  }

  anexar(id: string, file: File): void {
    this.novoAnexo = {
      nome: file.name,
      anexoId: id,
    };
    const clone = Object.assign({}, this.novoAnexo);
    this.anexos.push(clone);
    this.anexosChange.emit(this.anexos);
  }

  download(anexo: AnexoFk): void {
    event.stopPropagation();
    this.downloadAnexo.emit(anexo);
  }
}

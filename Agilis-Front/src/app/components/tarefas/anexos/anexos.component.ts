import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { constantes } from 'src/app/consts/constantes';
import { Anexo } from 'src/app/models/anexo';
import { Arquivo } from 'src/app/models/arquivo';
import { ArquivoApiService } from 'src/app/services/apis/arquivo-api.service';

@Component({
  selector: 'app-anexos',
  templateUrl: './anexos.component.html',
  styleUrls: ['./anexos.component.scss']
})
export class AnexosComponent {

  novoAnexo: Anexo;
  @Input() anexos: Anexo[] = [];
  @Output() anexosChange = new EventEmitter<Anexo[]>();
  @Output() downloadAnexo = new EventEmitter<Anexo>();

  constructor(
    private snackBar: MatSnackBar,
    private arquivoApiService: ArquivoApiService,
  ) { }

  excluir(indice: number) {
    const anexoRemovido = this.anexos[indice];

    this.arquivoApiService.excluir(anexoRemovido.arquivoId)
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

        const arquivo: Arquivo = {
          id: constantes.newGuid,
          base64: reader.result.valueOf().toString(),
          nome: file.name
        };

        this.upload(arquivo, file);

      };
      reader.onerror = () => {
        this.snackBar.open('Erro ao carregar o anexo');
      };
      reader.readAsDataURL(file);
    }
  }

  upload(arquivo: Arquivo, file: File) {
    this.arquivoApiService.adicionar(arquivo)
      .subscribe({
        next: id => this.anexar(id, file)
      });
  }

  anexar(id: string, file: File): void {
    this.novoAnexo = {
      nome: file.name,
      arquivoId: id,
      imagem: false
    };
    const clone = Object.assign({}, this.novoAnexo);
    this.anexos.push(clone);
    this.anexosChange.emit(this.anexos);
  }

  download(anexo: Anexo): void {
    event.stopPropagation();
    this.downloadAnexo.emit(anexo);
  }
}

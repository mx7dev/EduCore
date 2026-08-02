import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { httpResource } from '@angular/common/http';
import { NotaService, LibretaDto } from '../../../core/services/nota';
import { ResponseDto } from '../../../core/services/response';
import { Button } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { InputText } from 'primeng/inputtext';
import { NotaForm } from '../nota-form/nota-form';

@Component({
  selector: 'app-nota-list',
  imports: [Button, TableModule, InputText, FormsModule, NotaForm],
  templateUrl: './nota-list.html',
  styleUrl: './nota-list.scss'
})
export class NotaList {
  private notaService = inject(NotaService);

  matriculaIdInput = '';
  matriculaId = signal<number | null>(null);
  mostrarFormulario = false;

  libretaResponse = httpResource<ResponseDto<LibretaDto>>(() => {
    const id = this.matriculaId();
    if (!id) return undefined;
    return this.notaService.getLibretaUrl(id);
  });

  get libreta() {
    return () => this.libretaResponse.value()?.data ?? null;
  }

  buscarLibreta() {
  const id = Number(this.matriculaIdInput);
  if (id > 0) {
    if (this.matriculaId() === id) {
      // mismo id — forzar reload
      this.libretaResponse.reload();
    } else {
      this.matriculaId.set(id);
    }
  }
}

 abrirFormulario() {
  this.mostrarFormulario = true;
}
}
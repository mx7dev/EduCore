import { Component, inject } from '@angular/core';
import { httpResource } from '@angular/common/http';
import { GradoService, GradoDto } from '../../../core/services/grado';
import { ResponseDto } from '../../../core/services/response';
import { Button } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { GradoForm } from '../grado-form/grado-form';

@Component({
  selector: 'app-grado-list',
  imports: [Button, TableModule, GradoForm],
  templateUrl: './grado-list.html',
  styleUrl: './grado-list.scss'
})
export class GradoList {
  private gradoService = inject(GradoService);

  gradosResponse = httpResource<ResponseDto<GradoDto[]>>(() => this.gradoService.apiUrl);

  mostrarFormulario = false;

  get grados(): GradoDto[] {
    return [...(this.gradosResponse.value()?.data ?? [])];
  }

  abrirFormulario() {
    this.mostrarFormulario = true;
  }
}
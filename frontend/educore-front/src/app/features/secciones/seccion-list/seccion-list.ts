import { Component, inject } from '@angular/core';
import { httpResource } from '@angular/common/http';
import { SeccionService, SeccionDto } from '../../../core/services/seccion';
import { ResponseDto } from '../../../core/services/response';
import { Button } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { SeccionForm } from '../seccion-form/seccion-form';

@Component({
  selector: 'app-seccion-list',
  imports: [Button, TableModule, SeccionForm],
  templateUrl: './seccion-list.html',
  styleUrl: './seccion-list.scss'
})
export class SeccionList {
  private seccionService = inject(SeccionService);

  seccionesResponse = httpResource<ResponseDto<SeccionDto[]>>(() => this.seccionService.apiUrl);
  mostrarFormulario = false;

  get secciones(): SeccionDto[] {
    return this.seccionesResponse.value()?.data ?? [];
  }

  abrirFormulario() {
    this.mostrarFormulario = true;
  }
}
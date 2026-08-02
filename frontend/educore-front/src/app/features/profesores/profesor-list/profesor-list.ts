import { Component, inject } from '@angular/core';
import { httpResource } from '@angular/common/http';
import { ProfesorService, ProfesorDto } from '../../../core/services/profesor';
import { Button } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { ProfesorForm } from '../profesor-form/profesor-form';
import { ResponseDto } from '../../../core/services/response';

@Component({
  selector: 'app-profesor-list',
  imports: [Button, TableModule, ProfesorForm],
  templateUrl: './profesor-list.html',
  styleUrl: './profesor-list.scss'
})
export class ProfesorList {
  private profesorService = inject(ProfesorService);
  
  
  profesoresResponse = httpResource<ResponseDto<ProfesorDto[]>>(() => this.profesorService.apiUrl);
  mostrarFormulario = false;
  get profesores(): ProfesorDto[] {
    return this.profesoresResponse.value()?.data ?? [];
  }

  abrirFormulario() {
    this.mostrarFormulario = true;
  }

  eliminar(id: number) {
    if (confirm('¿Estás seguro de eliminar este profesor?')) {
      this.profesorService.eliminar(id).subscribe({
        next: () => this.profesoresResponse.reload()
      });
    }
  }
}
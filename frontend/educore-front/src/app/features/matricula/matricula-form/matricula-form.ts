import { Component, EventEmitter, inject, Input, Output, signal } from '@angular/core';
import { form, FormField, required, submit } from '@angular/forms/signals';
import { MatriculaService, CrearMatriculaDto } from '../../../core/services/matricula';
import { Button } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputText } from 'primeng/inputtext';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-matricula-form',
  imports: [Button, Dialog, InputText, FormField, Toast],
  providers: [MessageService],
  templateUrl: './matricula-form.html',
  styleUrl: './matricula-form.scss'
})
export class MatriculaForm {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() matriculaGuardada = new EventEmitter<void>();

  private matriculaService = inject(MatriculaService);
  private messageService = inject(MessageService);
  guardando = false;

  modelo = signal({
    alumnoId: '',
    seccionId: ''
  });

  formulario = form(this.modelo, (path) => {
    required(path.alumnoId, { message: 'El alumno es obligatorio' });
    required(path.seccionId, { message: 'La sección es obligatoria' });
  });

  cerrar() {
    this.visible = false;
    this.visibleChange.emit(false);
  }

  guardar() {
    submit(this.formulario, async () => {
      this.guardando = true;
      const v = this.modelo();
      const dto: CrearMatriculaDto = {
        alumnoId: Number(v.alumnoId),
        seccionId: Number(v.seccionId)
      };

      this.matriculaService.crear(dto).subscribe({
        next: (response: any) => {
          this.guardando = false;
          if (response.success) {
            this.messageService.add({ severity: 'success', summary: 'Éxito', detail: response.message });
            this.matriculaGuardada.emit();
            this.cerrar();
          } else {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: response.message });
          }
        },
        error: () => {
          this.guardando = false;
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Ocurrió un error inesperado' });
        }
      });
    });
  }
}
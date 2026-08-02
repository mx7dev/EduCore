import { Component, EventEmitter, inject, Input, Output, signal } from '@angular/core';
import { form, FormField, required, submit } from '@angular/forms/signals';
import { FormsModule } from '@angular/forms';
import { GradoService, CrearGradoDto } from '../../../core/services/grado';
import { Button } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputText } from 'primeng/inputtext';
import { Select } from 'primeng/select';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-grado-form',
  imports: [Button, Dialog, InputText, FormField, Select, FormsModule, Toast],
  providers: [MessageService],
  templateUrl: './grado-form.html',
  styleUrl: './grado-form.scss'
})
export class GradoForm {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() gradoGuardado = new EventEmitter<void>();

  private gradoService = inject(GradoService);
  private messageService = inject(MessageService);
  guardando = false;
  nivelSeleccionado = 1;

  niveles = [
    { label: 'Primaria', value: 1 },
    { label: 'Secundaria', value: 2 }
  ];

  modelo = signal({ numero: '' });

  formulario = form(this.modelo, (path) => {
    required(path.numero, { message: 'El número de grado es obligatorio' });
  });

  cerrar() {
    this.visible = false;
    this.visibleChange.emit(false);
    this.modelo.set({ numero: '' });
    this.nivelSeleccionado = 1;
  }

  guardar() {
    submit(this.formulario, async () => {
      this.guardando = true;
      const dto: CrearGradoDto = {
        numero: Number(this.modelo().numero),
        nivel: this.nivelSeleccionado
      };

      this.gradoService.crear(dto).subscribe({
        next: (response: any) => {
          this.guardando = false;
          if (response.success) {
            this.messageService.add({ severity: 'success', summary: 'Éxito', detail: response.message });
            this.gradoGuardado.emit();
            this.cerrar();
          } else {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: response.message });
          }
        },
        error: () => {
          this.guardando = false;
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error inesperado' });
        }
      });
    });
  }
}
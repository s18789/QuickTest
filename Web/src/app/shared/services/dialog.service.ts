import { ComponentType, Overlay } from "@angular/cdk/overlay";
import { ComponentPortal, PortalInjector } from "@angular/cdk/portal";
import { ComponentRef, Injectable, Injector } from "@angular/core";

@Injectable({ providedIn: "root" })

export class DialogService {
  constructor(
    private _injector: Injector,
    private _overlay: Overlay
  ) { }

  openDialog<T>(component: ComponentType<T>, token: object): ComponentRef<T> {
    const overlayRef = this._overlay.create({
      positionStrategy: this._overlay
        .position()
        .global()
        .centerVertically(),
    });

    const map = new WeakMap();
    map.set(token, overlayRef);
    this._injector

    const portal = new ComponentPortal(
      component,
      null,
      new PortalInjector(this._injector, map),
    );
    return overlayRef.attach(portal);
  }
}

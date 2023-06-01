import { Injectable } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root',
})
export class CustomIconService {
  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer
  ) {}

  public init() {
    //Sidebar icons
    this.matIconRegistry.addSvgIcon(
      'personal',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/Personal.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      'share',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/Share.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      'settings',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/Settings.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      'team',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/Team.svg'
      )
    );

    //Horizontalbar icons
    this.matIconRegistry.addSvgIcon(
      'search',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/Search.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      'bell',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/Bell.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      'menu',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/Menu.svg'
      )
    );

    //Common
    this.matIconRegistry.addSvgIcon(
      'share-link',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/ShareLink.svg'
      )
    );
    this.matIconRegistry.addSvgIcon(
      'details',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/Details.svg'
      )
    );

    //Personal page icons
    this.matIconRegistry.addSvgIcon(
      'star',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/Star.svg'
      )
    );
     this.matIconRegistry.addSvgIcon(
       'vertical-dots',
       this.domSanitizer.bypassSecurityTrustResourceUrl(
         '../assets/icons/verticalDots.svg'
       )
     );

    //Toggle button group  icons
    this.matIconRegistry.addSvgIcon(
      'list',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/layoutList.svg'
      )
    );

    this.matIconRegistry.addSvgIcon(
      'grid',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/layoutGrid.svg'
      )
    );

    //List view folder icon
    this.matIconRegistry.addSvgIcon(
      'folder',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        '../assets/icons/folder.svg'
      )
    );
  }
}

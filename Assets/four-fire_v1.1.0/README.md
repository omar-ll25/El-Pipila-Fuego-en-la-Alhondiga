# Four Fire

16-bit style animated effect sprites.

- **Multiple cell sizes: 32x32 px, 64x64 px, 128x128 px**, Standard tier: 32x32 px, Premium tier(s): 64x64, 128x128 px
- **Anchor:** center - the same point in every frame, so the
  sprite never slides inside its cell
- **Perspective:** effect
- **Palette:** 37 colours, locked across every frame
- **Transparency:** binary alpha only (0 or 255), safe for point filtering

## Animations

| Clip | Frames | FPS | Loops | Location in combined sheet |
|------|--------|-----|-------|----------------------------|
| `wide-fire_normal_32` | 16 | 12 | yes | row 0 |
| `wide-fire_red_32` | 16 | 12 | yes | row 0 |
| `wide-fire_blue_32` | 16 | 12 | yes | row 0 |
| `wide-fire_green_32` | 16 | 12 | yes | row 0 |
| `thin-fire_normal_32` | 16 | 12 | yes | row 0 |
| `thin-fire_red_32` | 16 | 12 | yes | row 0 |
| `thin-fire_blue_32` | 16 | 12 | yes | row 0 |
| `thin-fire_green_32` | 16 | 12 | yes | row 0 |

## Files

```
sheets/four-fire_sheet.png            all clips, one row per clip
sheets/four-fire_sheet.json           grid + clip metadata
sheets/four-fire_sheet.aseprite.json  Aseprite array format, with frameTags
sheets/four-fire_<clip>.png           one sheet per clip, single row
sheets/four-fire_*_n.png              normal maps, if the pack ships them:
                                              same grid and same cell as the sheet
                                              they sit beside, so one importer
                                              setting covers both
frames/<clip>/000.png                         individual frames
preview/                                      gifs and contact sheet
```

See `LICENSE.txt` for usage terms.

This is the **Standard** edition. Held back for Premium: thin-fire_blue_128, thin-fire_blue_64, thin-fire_green_128, thin-fire_green_64, thin-fire_normal_128, thin-fire_normal_64, thin-fire_red_128, thin-fire_red_64, wide-fire_blue_128, wide-fire_blue_64, wide-fire_green_128, wide-fire_green_64, wide-fire_normal_128, wide-fire_normal_64, wide-fire_red_128, wide-fire_red_64.

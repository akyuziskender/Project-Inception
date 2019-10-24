using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

	public Tilemap DarkMap;
	public Tilemap BlurredMap;
	public Tilemap CaveBackground;

	public Tile DarkTile;
	public Tile BlurredTile;

	// Start is called before the first frame update
	void Start() {
		DarkMap.origin = BlurredMap.origin = CaveBackground.origin;
		DarkMap.size = BlurredMap.size = CaveBackground.size;

		for (int n = CaveBackground.cellBounds.xMin; n < CaveBackground.cellBounds.xMax; n++) {
			for (int p = CaveBackground.cellBounds.yMin; p < CaveBackground.cellBounds.yMax; p++) {
				Vector3Int localPlace = (new Vector3Int(n, p, (int)CaveBackground.transform.position.z));
				Vector3 place = CaveBackground.CellToWorld(localPlace);
				Vector3Int pos = (new Vector3Int((int)place.x, (int)place.y, (int)place.z));
				if (CaveBackground.HasTile(localPlace)) {
					DarkMap.SetTile(pos, DarkTile);
					BlurredMap.SetTile(pos, BlurredTile);
				}
			}
		}
	}
}

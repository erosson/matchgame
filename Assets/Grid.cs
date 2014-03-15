using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	public int width = 10;
	public int height = 10;
	public int minMatch = 3;
	public GameObject blockPrefab;
	
	public delegate void MatchEventHandler(HashSet<Block> blocks);
	public event MatchEventHandler MatchEvent = delegate {};
	public event MatchEventHandler ClearEvent = delegate {};
	
	private Dictionary<IntVector2, Block> blockDict = new Dictionary<IntVector2, Block>();

	private T RandomElement<T>(T[] elements) {
		return elements[Random.Range(0, elements.Length)];
	}

	// Use this for initialization
	void Start() {
		MatchEvent += matches => StartCoroutine(OnMatch(matches));
		ClearEvent += matches => StartCoroutine(OnClear(matches));

		var blocks = new HashSet<Block>();
		for (int x=0; x < width; x++) {
			for (int y=0; y < height; y++) {
				blocks.Add(CreateRandomBlock(x, y));
			}
		}
		FindAndClearMatches(blocks);
	}

	private Block CreateRandomBlock(int x, int y) {
		return CreateRandomBlock(new IntVector2(x, y));
	}
	private Block CreateRandomBlock(IntVector2 location) {
		var blockObject = Instantiate(blockPrefab, new Vector3(location.X, location.Y, 0), Quaternion.identity) as GameObject;
		var block = blockObject.GetComponent<Block>();
		block.matchType = RandomElement(System.Enum.GetValues(typeof(Block.MatchType)) as Block.MatchType[]);
		blockObject.transform.parent = transform;
		block.DropEvent += OnDrop;
		blockDict[location] = block;
		return block;
	}

	private void OnDrop(Block block, IntVector2 start, IntVector2 end) {
		var diff = end - start;
		var starts = new HashSet<IntVector2>();
		if (diff.X != 0) {
			// assert diff.Y == 0;
			for (int x=0; x < width; x++) {
				starts.Add(new IntVector2(x, block.Position.Y));
			}
		}
		else if (diff.Y != 0) {
			for (int y=0; y < height; y++) {
				starts.Add(new IntVector2(block.Position.X, y));
			}
		}

		var moved = new HashSet<Block>();
		foreach (var startLoc in starts) {
			var endLoc = new IntVector2((startLoc.X + diff.X + width) % width, (startLoc.Y + diff.Y + height) % height);
			var b = blockDict[startLoc];
			moved.Add(b);
			blockDict.Remove(startLoc);
			b.Position = endLoc;
		}
		foreach (Block b in moved) {
			blockDict.Add(b.Position, b);
		}

		FindAndClearMatches(moved);
	}

	private void FindAndClearMatches(HashSet<Block> blocks) {
		var matches = FindMatches(blocks);
		if (matches.Count > 0) {
			MatchEvent(matches);
		}
	}

	private IEnumerator OnMatch(HashSet<Block> matches) {
		foreach (var match in matches) {
			match.GetComponent<Animator>().SetTrigger("Match");
			// Allowing the dragging block to match is dangerous, because it'll disappear while we're dragging it
			DebugUtil.Assert(
				match.state != Block.State.Dragging,
			    "match contains the dragging block - but per the definition of IsMatchable, that's impossible!");
			match.state = Block.State.Matching;
		}
		yield return new WaitForSeconds(1.0f);
		ClearEvent(matches);
	}

	private IEnumerator OnClear(HashSet<Block> matches) {
		// clear matched blocks
		foreach (var match in matches) {
			var dictval = blockDict[match.Position];
			DebugUtil.Assert(dictval == match, "cleared block's location in the blockdict is wrong");
			blockDict.Remove(match.Position);
			DestroyObject(match.gameObject);
		}
		// gravity. TODO
		// For now, just fill in the cleared blocks
		var newblocks = new HashSet<Block>();
		foreach (var match in matches) {
			newblocks.Add(CreateRandomBlock(match.Position));
		}
		FindAndClearMatches(newblocks);
		yield return null;
	}

	private HashSet<Block> FindMatches(HashSet<Block> blocks) {
		// unity, why you no support .net 4, with collection.sum()
		var ret = new HashSet<Block>();
		foreach (var block in blocks) {
			ret.UnionWith(FindMatches(block));
		}
		return ret;
	}

	private HashSet<Block> FindMatches(Block block) {
		var ret = new HashSet<Block>();
		ret.UnionWith(FindMatches(block, new IntVector2(1, 0)));
		ret.UnionWith(FindMatches(block, new IntVector2(0, 1)));
		return ret;
	}

	private HashSet<Block> FindMatches(Block block, IntVector2 direction) {
		var matches = new HashSet<Block>();
		matches.Add(block);
		var dirs = new IntVector2[]{direction, -direction};
		foreach (IntVector2 dir in dirs) {
			var pos = block.Position + dir;
			while (IsInBounds(pos) && block.IsMatching(blockDict[pos])) {
				matches.Add(blockDict[pos]);
				pos += dir;
			}
		}
		if (matches.Count < minMatch) {
			matches.Clear();
		}
		return matches;
	}

	private bool IsInBounds(IntVector2 point) {
		return point.X >= 0 && point.X < width && point.Y >= 0 && point.Y < height;
	}
}

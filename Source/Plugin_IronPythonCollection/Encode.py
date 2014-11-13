import clr
clr.AddReference('Altman.Plugin')
from Altman.Plugin import PluginServiceProvider
from Altman.Plugin.Interface import IService
import sys,os

@export(IService)
class Service(IService):
    def Load(self):
        #PluginServiceProvider.RegisterService("ToHex", "Encode", hex_encode)
        #PluginServiceProvider.RegisterService("FromHex", "Decode", hex_decode)
        PluginServiceProvider.RegisterService("ToFingerBinary", "Encode", finger_binary)
        return True

def hex_encode(args):
    return args[0].encode('hex')

def hex_decode(args):
    return args[0].decode('hex')

r"""
=============
Finger Binary
=============

What's Finger Binary?
`````````````````````
 
Here is a description of `Finger Binary in Wikipedia`_.
 
    Finger binary is a system for counting and displaying binary numbers on the
    fingers and thumbs of one or more hands. It is possible to count from 0 to
    31(2^5−1) using the fingers of a single hand, or from 0 through
    1023(2^10−1) if both hands are used.
 
.. _Finger Binary in WikiPedia: http://en.wikipedia.org/wiki/Finger_binary
 
"""
from textwrap import dedent

__all__ = "finger_binary",


WIDTH = 24
RIGHT_HANDS = [
    # 0 --------------------
    r"""
                   __
              ___ /  |
        __.--|   \   |
     __/  \   \   |  |
    /  \   |   |  |  \
    |   |  |   |  |   |
    |\  |  |   |  |   |
    | '-\__'__/--'   |
    \               /
     \             /
      |            |
    """,
    # 1 -------------------
    r"""
              ___
        __.--|   \
     __/  \   \   |
    /  \   |   |  |   _____
    |   |  |   |  |-''     |
    |\  |  |   |  |      _/
    | '-\__'__/--'   _-''
    \         (     /
     \             /
      |            |
    """,
    # 2 -------------------
    r"""
              __
             /  \
             |   |
             |   |
             |   |
             |   |
        __.---.  |
     __/  \  __\_|___
    /  \   |/        \
    |   |  |______    |
    |\  |  |   |      |
    | '-\__'__/      |
    \         (     /
     \             /
      |            |
    """,
    # 3 -------------------
    r"""
              __
             /  \
             |   |
             |   |
             |   |
             |   |
        __.---_  |
     __/  \    \ |
    /  \   |   |  |   _____
    |   |  |   |  \-''     |
    |\  |  |   |         _/
    | '-\__'__/      _-''
    \         (     /
     \             /
      |            |
    """,
    # 4 -------------------
    r"""
           __
          /  \
          |   |
          |   |
          |   |
          |   |
        __|   |__
     __/  \  _/__\___
    /  \   |/        \
    |   |  |______    |
    |\  |  |  |  |    |
    | '-\__'  '--'   |
    \         (     /
     \             /
      |            |
    """,
    # 5 -------------------
    r"""
           __
          /  \
          |   |
          |   |
          |   |
          |   |
        __|   |__
     __/  \   /  \
    /  \   |  |  |\   _____
    |   |  |  |  | -''     |
    |\  |  |  |  |       _/
    | '-\__'  '--'   _-''
    \         (     /
     \             /
      |            |
    """,
    # 6 -------------------
    r"""
           __ __
          /  V  \
          |  |   |
          |  |   |
          |  |   |
          |  |   |
        __|  |   |
     __/  \  |___|___
    /  \   |/        \
    |   |  |______    |
    |\  |  |          |
    | '-\__'   /     |
    \         (     /
     \             /
      |            |
    """,
    # 7 -------------------
    r"""
           __ __
          /  V  \
          |  |   |
          |  |   |
          |  |   |
          |  |   |
        __|  |   |
     __/  \  |   |
    /  \   |      |   _____
    |   |  |      \-''     |
    |\  |  |             _/
    | '-\__'   /     _-''
    \         (     /
     \             /
      |            |
    """,
    # 8 -------------------
    r"""
        _
       / \
       |  |
       |  |        __
       |  |   ___ /  |
       |  |--|   \   |
     __|  \   \   |  |
    /  \   |   |  |  \
    |   |  |   |  |   |
    |\  |  |   |  |   |
    | '-'  '__/--'   |
    \               /
     \             /
      |            |
    """,
    # 9 -------------------
    r"""
        _
       / \
       |  |
       |  | 
       |  |   ___
       |  |--|   \
     __|  \   \   |
    /  \   |   |  |   _____
    |   |  |   |  |-''     |
    |\  |  |   |  |      _/
    | '-'  '__/--'   _-''
    \         (     /
     \             /
      |            |
    """,
    # 10 ------------------
    r"""
              __
        _    /  \
       / \   |   |
       |  |  |   |
       |  |  |   |
       |  |  |   |
       |  |---.  |
     __|  \  __\_|___
    /  \   |/        \
    |   |  |______    |
    |\  |  |   |      |
    | '-'  '__/      |
    \         (     /
     \             /
      |            |
    """,
    # 11 ------------------
    r"""
              __
        _    /  \
       / \   |   |
       |  |  |   |
       |  |  |   |
       |  |  |   |
       |  |---_  |
     __|  \    \ |
    /  \   |   |  |   _____
    |   |  |   |  \-''     |
    |\  |  |   |         _/
    | '-'  '__/      _-''
    \         (     /
     \             /
      |            |
    """,
    # 12 ------------------
    r"""
           __
        _ /  \
       / \|   |
       |  |   |
       |  |   |
       |  |   |
       |  |   |__
     __|  |  _/__\___
    /  \    /        \
    |   |  |______    |
    |\  |     |  |    |
    | '-'     '--'   |
    \         (     /
     \             /
      |            |
    """,
    # 13 ------------------
    r"""
           __
        _ /  \
       / \|   |
       |  |   |
       |  |   |
       |  |   |
       |  |   |__
     __|  |   /  \
    /  \      |  |\   _____
    |   |     |  | -''     |
    |\  |     |  |       _/
    | '-'     '--'   _-''
    \         (     /
     \             /
      |            |
    """,
    # 14 ------------------
    r"""
           __ __
        _ /  V  \
       / \|  |   |
       |  |  |   |
       |  |  |   |
       |  |  |   |
       |  |  |   |
     __|  |  |___|___
    /  \    /        \
    |   |  |______    |
    |\  |             |
    | '-'      /     |
    \         (     /
     \             /
      |            |
    """,
    # 15 ------------------
    r"""
           __ __
        _ /  V  \
       / \|  |   |
       |  |  |   |
       |  |  |   |
       |  |  |   |
       |  |  |   |
     __|  |  |   |
    /  \          |   _____
    |   |         \-''     |
    |\  |                _/
    | '-'      /     _-''
    \         (     /
     \             /
      |            |
    """,
    # 16 ------------------
    r"""
     _
    / \
    |  |           __
    |  |      ___ /  |
    |  |__.--|   \   |
    |  |  \   \   |  |
    |  \   |   |  |  \
    |   |  |   |  |   |
    |   |  |   |  |   |
    |   \__'__/--'   |
    \               /
     \             /
      |            |
    """,
    # 17 ------------------
    r"""
     _
    / \
    |  |
    |  |      ___
    |  |__.--|   \
    |  |  \   \   |
    |  \   |   |  |   _____
    |   |  |   |  |-''     |
    |   |  |   |  |      _/
    |   \__'__/--'   _-''
    \         (     /
     \             /
      |            |
    """,
    # 18 ------------------
    r"""
              __
             /  \
     _       |   |
    / \      |   |
    |  |     |   |
    |  |     |   |
    |  |__.---.  |
    |  |  \  __\_|___
    |  \   |/        \
    |   |  |______    |
    |   |  |   |      |
    |   \__'__/      |
    \         (     /
     \             /
      |            |
    """,
    # 19 ------------------
    r"""
              __
             /  \
     _       |   |
    / \      |   |
    |  |     |   |
    |  |     |   |
    |  |__.---_  |
    |  |  \    \ |
    |  \   |   |  |   _____
    |   |  |   |  \-''     |
    |   |  |   |         _/
    |   \__'__/      _-''
    \         (     /
     \             /
      |            |
    """,
    # 20 ------------------
    r"""
           __
          /  \
     _    |   |
    / \   |   |
    |  |  |   |
    |  |  |   |
    |  |__|   |__
    |  |  \  _/__\___
    |  \   |/        \
    |   |  |______    |
    |   |  |  |  |    |
    |   \__'  '--'   |
    \         (     /
     \             /
      |            |
    """,
    # 21 ------------------
    r"""
           __
          /  \
     _    |   |
    / \   |   |
    |  |  |   |
    |  |  |   |
    |  |__|   |__
    |  |  \   /  \
    |  \   |  |  |\   _____
    |   |  |  |  | -''     |
    |   |  |  |  |       _/
    |   \__'  '--'   _-''
    \         (     /
     \             /
      |            |
    """,
    # 22 ------------------
    r"""
           __ __
          /  V  \
     _    |  |   |
    / \   |  |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |__|  |   |
    |  |  \  |___|___
    |  \   |/        \
    |   |  |______    |
    |   |  |          |
    |   \__'   /     |
    \         (     /
     \             /
      |            |
    """,
    # 23 ------------------
    r"""
           __ __
          /  V  \
     _    |  |   |
    / \   |  |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |__|  |   |
    |  |  \  |   |
    |  \   |      |   _____
    |   |  |      \-''     |
    |   |  |             _/
    |   \__'   /     _-''
    \         (     /
     \             /
      |            |
    """,
    # 24 ------------------
    r"""
      _ _
     / V \
    |  |  |
    |  |  |        __
    |  |  |   ___ /  |
    |  |  |--|   \   |
    |  |  \   \   |  |
    |  |   |   |  |  \
    |      |   |  |   |
    |      |   |  |   |
    |      '__/--'   |
    \               /
     \             /
      |            |
    """,
    # 25 ------------------
    r"""
      _ _
     / V \
    |  |  |
    |  |  | 
    |  |  |   ___
    |  |  |--|   \
    |  |  \   \   |
    |  |   |   |  |   _____
    |      |   |  |-''     |
    |      |   |  |      _/
    |      '__/--'   _-''
    \         (     /
     \             /
      |            |
    """,
    # 26 ------------------
    r"""
              __
      _ _    /  \
     / V \   |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |  |---.  |
    |  |  \  __\_|___
    |  |   |/        \
    |      |______    |
    |      |   |      |
    |      '__/      |
    \         (     /
     \             /
      |            |
    """,
    # 27 ------------------
    r"""
              __
      _ _    /  \
     / V \   |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |  |---_  |
    |  |  \    \ |
    |  |   |   |  |   _____
    |      |   |  \-''     |
    |      |   |         _/
    |      '__/      _-''
    \         (     /
     \             /
      |            |
    """,
    # 28 ------------------
    r"""
           __
      _ _ /  \
     / V \|   |
    |  |  |   |
    |  |  |   |
    |  |  |   |
    |  |  |   |__
    |  |  |  _/__\___
    |  |    /        \
    |      |______    |
    |         |  |    |
    |         '--'   |
    \         (     /
     \             /
      |            |
    """,
    # 29 ------------------
    r"""
           __
      _ _ /  \
     / V \|   |
    |  |  |   |
    |  |  |   |
    |  |  |   |
    |  |  |   |__
    |  |  |   /  \
    |  |      |  |\   _____
    |         |  | -''     |
    |         |  |       _/
    |         '--'   _-''
    \         (     /
     \             /
      |            |
    """,
    # 30 ------------------
    r"""
           __ __
      _ _ /  V  \
     / V \|  |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |  |  |___|___
    |  |    /        \
    |      |______    |
    |                 |
    |          /     |
    \         (     /
     \             /
      |            |
    """,
    # 31 ------------------
    r"""
           __ __
      _ _ /  V  \
     / V \|  |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |  |  |   |
    |  |          |   _____
    |             \-''     |
    |                    _/
    |          /     _-''
    \         (     /
     \             /
      |            |
    """
]


def reversed_hand(hand):
    reversing_chars = {"\\": "/", "(": ")"}
    rhand = []
    for line in hand.split("\n"):
        rline = line.ljust(WIDTH)[::-1]
        for lc, rc in reversing_chars.items():
            rline = rline.replace(lc, "\n").replace(rc, lc).replace("\n", rc)
        rhand.append(rline)
    return "\n".join(rhand)


def two_hands(lhand, rhand):
    lhand, rhand = lhand.split("\n"), rhand.split("\n")
    lheight, rheight = len(lhand), len(rhand)
    diff = abs(lheight - rheight)
    if diff:
        short_hand = lhand if lheight < rheight else rhand
        short_hand.reverse()
        short_hand.extend([""] * diff)
        short_hand.reverse()
    hands = []
    for x in xrange(max(lheight, rheight)):
        hands.append(lhand[x].ljust(WIDTH) + rhand[x])
    return "\n".join(hands)


def finger_binary(args):
    try:
        n = int(args[0])
    except:
        return "please input number"
    max_of_a_hand = 32
    if n >= max_of_a_hand ** 2 or n < 0:
        limit = max_of_a_hand ** 2 - 1
        return "number must be between 0 and " + str(limit)
    fingers = [n / max_of_a_hand, n % max_of_a_hand]
    fingers[0] = int(bin(max_of_a_hand + fingers[0])[3:][::-1], 2)
    hands = (reversed_hand(dedent(RIGHT_HANDS[fingers[0]])),
             dedent(RIGHT_HANDS[fingers[1]]))
    return two_hands(*hands)
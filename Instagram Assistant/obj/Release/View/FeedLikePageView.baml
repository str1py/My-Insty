   M S B A M L   `   `   `  ���� R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel   Instagram Assistant/   )Instagram_Assistant.View.FeedLikePageView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      @d2http://schemas.microsoft.com/expression/blend/2008      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  Bscm8clr-namespace:System.ComponentModel;assembly=WindowsBase    � 	Resources     LikeActionTemplate J� Opacity �� CornerRadius J� Effect-  'System.Windows.Media.Effects.BlurEffect   Radius   
KernelType �� Orientation  SecondColorBrush j� Stretch �� ImageSource  TextBlockThinStyle  Smaller!  FontAwesome.WPF.FontAwesome	  Name
   Icon � Triggers :� RoutedEvent �� TargetProperty g� Duration W� From	 W� To W� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness t� From	 t� To t� EasingFunction.  (System.Windows.Media.Animation.PowerEase   Power g� AccelerationRatio  likeCollection �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction  	FlashAnim �� 
TargetName  g� AutoReverse! g� RepeatBehavior  	ColorAnim  SecondColor	" �� To��%�  &	        &	 _    &	 �    &	 �    �� .R�� .+��� 
�������$
 0.05Q�$ 5��   $ 10Q�$ Gaussian=��� 
������ $ 5��._��� $ Vertical=�$	��150��$	��230��.+��� $��3,5,0,5q�$ 
Horizontal=�$
��Left=�.+�A� $��25��$��25��$��2��#���  ���� $ Uniform=� �� *AccountImage+�� $��5,0,0,0q�#���  #���  $��Center=�
����������� *AccountName+�� $	��160��$	��150��$��UniformToFill=����� *	PostImage+  $	 Action��
���  ��
 �� *Action+ :� $ Loaded��[��� .���� .��W� $ FontSize�$ 0:0:3D�$ 40X�$ 20X�   $ EaseIn=�$ 2��$ 1Q�t� $ Margin�$ 0:0:3.5D�$ 
0,-100,0,0X�$ 0,10,0,0X�   $ EaseIn=�$ 15Q� :� $ Loaded��[��� .���� .��t� $ Margin�$ 
-100,0,0,0X�$ 0X�$	 0.9Q�$ 0:0:0.5D�   $ EaseOut=�$ 2��$ 7Q���  �� $ Actions�   $
 Time��$ 
Descending=��� .��W� $ Status��$ Opacity�$ 1X�$	 0.3X�  . $ 0:0:1D�$! Forever���� .���� $ Status��$( "Foreground.(SolidColorBrush.Color)�$! Forever��  . $ 0:0:1D�#" �  .��� # � RowDefinitions# �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� $ � ColumnDefinitions$ �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��0��$��32��$��4��$��8��  TextBlockMainStyle#���  $��Center=�$��Center=� 	 H1#��� 	 
Overview�� $��0��$��5��$��2��$��0��#���   
 Regular#��� 
 Last actions�� $��0��$��11��$��11��$��3��$ 3��
�������$
 0.05Q�   $ 10Q�$ Gaussian=��� $��21��$��15��$��11��$��3��$ 3��
�������$
 0.05Q�   $ 10Q�$ Gaussian=��� $��11��$��14��$��4��$��3��$��Center=�$ Vertical=�.+���   MainButtonStyle#���  $��40������ *StartCommand+���� *ButtonContent+�� -   % ��Name$% Status��$��Center=�
�������$��0,20,0,5q� 
 Bigger#���  #���  ���� *Stats.Status+*  $Microsoft.Xaml.Behaviors.Interaction&   Triggers& /  )Microsoft.Xaml.Behaviors.Core.DataTrigger  '   Value$' WORKING��:	  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger( 	  Binding( �� *Stats.Status+*
  $Microsoft.Xaml.Behaviors.TriggerBase) 
  Actions.) <  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction  5  /Microsoft.Xaml.Behaviors.Media.StoryboardAction*   
Storyboard#* [ +   ControlStoryboardOption$
+ Play=�  $	' OFF��( �� *Stats.Status+.)   #* [ $
+ Stop=��� $��11��$��3��$��1��$��10��$��Center=�$ 
Horizontal=�.+��� $��Center=�.+��� $ 
Horizontal=�.+��� $ Vertical=�$��0,0,40,0q�.+��� $ 
Horizontal=�$��0,0,0,5q�.+�  $��0,0,5,0q�$
 Heart=�$��15(�
���  ��$��Center=�$��Center=��� $��Center=�
�������#���    TextBlockExtraThinStyle#���  ���� *Stats.SessionCount+�� $��Center=�#���  $��Session���� $ Vertical=�$��0,0,40,0q�.+��� $ 
Horizontal=�$��0,0,0,5q�.+�  $��0,0,5,0q�$
 Heart=�$��15(�
���  ��$��Center=�$��Center=��� $��Center=�
�������#���  #���  ���� *Stats.Count+�� $��Center=�#���  $��Total���� $��Center=�.+��� $��Center=�$��0,0,0,5q�
�������#���  #���  ���� *Stats.NextIn+�� $��Center=�#���  $��Next in���� $��11��$��3��$��22��$��9��$��Center=�$��Center=�$ 
Horizontal=�.+��� $��Center=�$��0,0,40,0q�.+��� $��Center=�$��0,0,0,5q�
�������#���  #���  ���� *Stats.TimeInWork+�� $��Center=�#���  $��Time in work���� $��Center=�.+��� $��Center=�$��0,0,0,5q�
�������#���  #���  ���� *Stats.NextSessionIn+, �� StringFormat$, hh\:mm���� $��Center=�#���  $��Next session in���� $��4��$��0��$��32��$��2��$	 0.6Q�  TextBlockH2Style#���  #��� 	 $��Center=�$��Center=����� *LastActionTextHelper+�� $��1��$��8��$��0��$��32��
������ $��0q�#���   ��.  $��Hidden=�$��Hidden=�!- �� IsDeferredScrollingEnabled- .  ���� . �� Source#. [ ���� .R�$� / $� Orientation$/ 
Horizontal=�
// Copyright 2014 Google Inc. All Rights Reserved.

#import "GADUBanner.h"

@import CoreGraphics;
@import UIKit;

#import "GADUPluginUtil.h"
#import "UnityAppController.h"

@interface GADUBanner () <GADBannerViewDelegate>

/// Defines where the ad should be positioned on the screen.
@property(nonatomic, assign) GADAdPosition adPosition;

@end

@implementation GADUBanner

- (id)initWithBannerClientReference:(GADUTypeBannerClientRef *)bannerClient
                           adUnitID:(NSString *)adUnitID
                              width:(CGFloat)width
                             height:(CGFloat)height
                         adPosition:(GADAdPosition)adPosition {
  GADAdSize adSize = GADAdSizeFromCGSize(CGSizeMake(width, height));
  return [self initWithBannerClientReference:bannerClient
                                    adUnitID:adUnitID
                                      adSize:adSize
                                  adPosition:adPosition];
}

- (id)initWithSmartBannerSizeAndBannerClientReference:(GADUTypeBannerClientRef *)bannerClient
                                             adUnitID:(NSString *)adUnitID
                                           adPosition:(GADAdPosition)adPosition {
  // Choose the correct Smart Banner constant according to orientation.
  UIDeviceOrientation currentOrientation = [UIApplication sharedApplication].statusBarOrientation;
  GADAdSize adSize;
  if (UIInterfaceOrientationIsPortrait(currentOrientation)) {
    adSize = kGADAdSizeSmartBannerPortrait;
  } else {
    adSize = kGADAdSizeSmartBannerLandscape;
  }
  return [self initWithBannerClientReference:bannerClient
                                    adUnitID:adUnitID
                                      adSize:adSize
                                  adPosition:adPosition];
}

- (id)initWithBannerClientReference:(GADUTypeBannerClientRef *)bannerClient
                           adUnitID:(NSString *)adUnitID
                             adSize:(GADAdSize)size
                         adPosition:(GADAdPosition)adPosition {
  self = [super init];
  if (self) {
    _bannerClient = bannerClient;
    _adPosition = adPosition;
    _bannerView = [[GADBannerView alloc] initWithAdSize:size];
    _bannerView.adUnitID = adUnitID;
    _bannerView.delegate = self;
    _bannerView.rootViewController = [GADUPluginUtil unityGLViewController];
  }
  return self;
}

- (void)dealloc {
  _bannerView.delegate = nil;
}

- (void)loadRequest:(GADRequest *)request {
  if (!self.bannerView) {
    NSLog(@"GoogleMobileAdsPlugin: BannerView is nil. Ignoring ad request.");
    return;
  }
  [self.bannerView loadRequest:request];
}

- (void)hideBannerView {
  if (!self.bannerView) {
    NSLog(@"GoogleMobileAdsPlugin: BannerView is nil. Ignoring call to hideBannerView");
    return;
  }
  self.bannerView.hidden = YES;
}

- (void)showBannerView {
  if (!self.bannerView) {
    NSLog(@"GoogleMobileAdsPlugin: BannerView is nil. Ignoring call to showBannerView");
    return;
  }
  self.bannerView.hidden = NO;
}

- (void)removeBannerView {
  if (!self.bannerView) {
    NSLog(@"GoogleMobileAdsPlugin: BannerView is nil. Ignoring call to removeBannerView");
    return;
  }
  [self.bannerView removeFromSuperview];
}

#pragma mark GADBannerViewDelegate implementation

- (void)adViewDidReceiveAd:(GADBannerView *)adView {
  // Remove existing banner view from superview.
  [self.bannerView removeFromSuperview];

  // Add the new banner view.
  self.bannerView = adView;

  /// Align the bannerView in the Unity view bounds.
  UIView *unityView = [GADUPluginUtil unityGLViewController].view;
  [GADUPluginUtil positionView:self.bannerView
                inParentBounds:unityView.bounds
                withAdPosition:self.adPosition];
  [unityView addSubview:self.bannerView];

  if (self.adReceivedCallback) {
    self.adReceivedCallback(self.bannerClient);
  }
}

- (void)adView:(GADBannerView *)view didFailToReceiveAdWithError:(GADRequestError *)error {
  if (self.adFailedCallback) {
    NSString *errorMsg = [NSString
        stringWithFormat:@"Failed to receive ad with error: %@", [error localizedFailureReason]];
    self.adFailedCallback(self.bannerClient, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
  }
}

- (void)adViewWillPresentScreen:(GADBannerView *)adView {
  if (self.willPresentCallback) {
    self.willPresentCallback(self.bannerClient);
  }
}

- (void)adViewWillDismissScreen:(GADBannerView *)adView {
  // Callback is not forwarded to Unity.
}

- (void)adViewDidDismissScreen:(GADBannerView *)adView {
  if (self.didDismissCallback) {
    self.didDismissCallback(self.bannerClient);
  }
}

- (void)adViewWillLeaveApplication:(GADBannerView *)adView {
  if (self.willLeaveCallback) {
    self.willLeaveCallback(self.bannerClient);
  }
}

@end
